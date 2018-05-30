using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnnonsonMVC.ViewModels;
using Domain.Interfaces;
using AnnonsonMVC.Utilities;
using Data.Appsettings;
using Microsoft.Extensions.Options;
using System;

namespace AnnonsonMVC.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articelService;
        private readonly ICategoryService _categoryService;
        private readonly IStoreService _storeService;
        private readonly ICompanyService _companyService;
        private readonly IStoreArticleService _storeArticleService;
        private readonly IArticleCategoryService _articleCategoryService;
        private readonly ImageService _imageService;
        private readonly SelectedStoresService _selectedStoresService;
        private readonly AddOrEditStoreArticle _addOrEditStoreArticle;
        private readonly AddOrEditCategoryArticle _addOrEditCategoryArticle;
        private readonly AppSettings _appSettings;

        public ArticlesController(IArticleService articleService, ICategoryService categoryService, IStoreService storeService, 
            ICompanyService companyService, IStoreArticleService storeArticleService, ImageService imageService, 
            SelectedStoresService selectedStoresService, AddOrEditStoreArticle addOrEditStoreArticle, 
            AddOrEditCategoryArticle addOrEditCategoryArticle, IOptions<AppSettings> appSettings,IArticleCategoryService articleCategoryService)
        {
            _articelService = articleService;
            _categoryService = categoryService;
            _storeService = storeService;
            _companyService = companyService;
            _storeArticleService = storeArticleService;
            _articleCategoryService = articleCategoryService;
            _imageService = imageService;
            _selectedStoresService = selectedStoresService;
            _addOrEditStoreArticle = addOrEditStoreArticle;
            _addOrEditCategoryArticle = addOrEditCategoryArticle;
            _appSettings = appSettings.Value;
        }

        public IActionResult Index()
        {
            var articles = _articelService.GetAll();
            var userArticles = articles.Where(x => x.UserId == 3);
            return View(userArticles.ToList());
        }

        public IActionResult Create()
        {
            var model = new ArticleCreateViewModel
            {
                PublishBegin = DateTime.Today,
                PublishEnd = DateTime.Today
            };
            ViewData["CompanyId"] = new SelectList(_companyService.GetAll(), "CompanyId", "Name");
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "CategoryId", "Name");
            ViewData["StoreId"] = new SelectList(_storeService.GetAll(), "StoreId", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var stores = _storeService.GetAll();
                if (model.StoreIds != null)
                {                    
                    model.Slug = _articelService.GenerateSlug(model.Name);
                    model.UserId = 3;

                    var selectedStoreListIds = _selectedStoresService.GetSelectedStoresList(model, stores);
                    var categoryId = model.CategoryId;          
                    var newArticle = Mapper.ViewModelToModelMapping.ActicleViewModelToArticle(model);
                 
                    _articelService.Add(newArticle);
                    
                    _addOrEditCategoryArticle.CreateCategoryArticle(newArticle, categoryId);

                    _addOrEditStoreArticle.CreateStoreArticles(newArticle, selectedStoreListIds);
                  
                    var imageDirectoryPath = _imageService.CreateDirectoryForImage(newArticle);

                    var imageFile = model.ImageFile;
                    var saveImageToPath = _imageService.SaveImageToDirectory(newArticle, imageDirectoryPath, imageFile);

                    newArticle.ImageWidths = _imageService.CreateResizeImagesToImageDirectory(imageFile, saveImageToPath, imageDirectoryPath);

                    _articelService.Update(newArticle);
                    _imageService.TryToDeleteOriginalImage(saveImageToPath);
                }
            }

            ViewData["CompanyId"] = new SelectList(_companyService.GetAll(), "CompanyId", "Company");
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "CategoryId", "Category");
            ViewData["StoreId"] = new SelectList(_storeService.GetAll(), "StoreId", "Store");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var article = _articelService.Find(id, "ArticleCategory.Category", "StoreArticle.Store");
            var model = Mapper.ModelToViewModelMapping.ArticleDetailViewModel(article);

            var companies = _companyService.GetAll();
            var company = companies.FirstOrDefault(x => x.CompanyId == model.CompanyId);
            model.Company = company;

            ViewBag.categoryNames = model.ArticleCategory.Select(x => x.Category.Name);
            ViewBag.storeNames = model.StoreArticle.Select(x => x.Store.Name);
            var categoryName = model.ArticleCategory.Select(x => x.Category.Name);

            ViewBag.Price = "-";
            ViewBag.mediaUrl = _appSettings.MediaUrl;
            if (article == null)
            {
                return NotFound();
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {     
            var article = _articelService.Find(id, "StoreArticle.Store", "ArticleCategory.Category");
            
            var model = Mapper.ModelToViewModelMapping.ArticleToArticleViewModel(article);

            foreach (var categoryId in model.ArticleCategory.Select(x =>x.CategoryId))
            {
                model.CategoryId = categoryId;
            }
            var stores = model.StoreArticle.Select(x => x.StoreId).ToArray();
            model.StoreIds = stores;
            ViewBag.mediaUrl = _appSettings.MediaUrl;
            ViewBag.Price = "-";

            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "CategoryId", "Name");
            ViewData["StoreId"] = new SelectList(_storeService.GetAll(), "StoreId", "Name", stores);
            ViewData["CompanyId"] = new SelectList(_companyService.GetAll(), "CompanyId", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticleEditViewModel model)
        {
            var article = _articelService.Find(model.ArticleId, "ArticleCategory.Category", "StoreArticle.Store");

            if (model.ImageFile == null)
            {
                model.ImageFileFormat = article.ImageFileFormat;
                model.ImageFileName = article.ImageFileName;
                model.ImagePath = article.ImagePath;
                model.ImageWidths = article.ImageWidths;
            }
            else
            {
                _imageService.DeleteArticleImages(article.ImagePath, article.ImageFileName);
            }

            if (ModelState.IsValid)
            {
                _addOrEditStoreArticle.EditStoreArticles(model);

                _addOrEditCategoryArticle.EditCategoryArticle(model);
                    
                model.Slug = _articelService.GenerateSlug(model.Name);
                model.UserId = article.UserId;

                article = Mapper.ViewModelToModelMapping.EditActicleViewModelToArticle(model, article);

                if (model.ImageFile != null)
                {    
                    var imageDirectoryPath = _imageService.CreateDirectoryForImage(article);

                    var imageFile = model.ImageFile;
                    var saveImageToPath = _imageService.SaveImageToDirectory(article, imageDirectoryPath, imageFile);

                    article.ImageWidths = _imageService.CreateResizeImagesToImageDirectory(imageFile, saveImageToPath, imageDirectoryPath);

                    _imageService.TryToDeleteOriginalImage(saveImageToPath);
                }
                try
                {
                    _articelService.Update(article);
                }
                catch (Exception)
                {
                    Console.WriteLine("Gick inte att uppdatera annonsen.");
                }
                return RedirectToAction(nameof(Index));
            }

            var stores = model.StoreArticle.Select(x => x.StoreId).ToArray();
            model.StoreIds = stores;
            ViewBag.mediaUrl = _appSettings.MediaUrl;
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "CategoryId", "Name");
            ViewData["StoreId"] = new SelectList(_storeService.GetAll(), "StoreId", "Name", stores);
            ViewData["CompanyId"] = new SelectList(_companyService.GetAll(), "CompanyId", "Name");

            return View(model);
        }
    }
}



//   ------------Tankar--------------

// Strukturen kring Utilities och servicar som är där vart skall dom ligga egentligen?






