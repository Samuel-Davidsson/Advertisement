using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnnonsonMVC.ViewModels;
using Domain.Interfaces;
using Domain.Entites;
using AnnonsonMVC.Utilities;
using Data.Appsettings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
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
        private readonly AppSettings _appSettings;

        public ArticlesController(IArticleService articleService, ICategoryService categoryService, IStoreService storeService, ICompanyService companyService, 
            IStoreArticleService storeArticleService, ImageService imageService, 
            SelectedStoresService selectedStoresService, IOptions<AppSettings> appSettings, IArticleCategoryService articleCategoryService)
        {
            _articelService = articleService;
            _categoryService = categoryService;
            _storeService = storeService;
            _companyService = companyService;
            _storeArticleService = storeArticleService;
            _articleCategoryService = articleCategoryService;
            _imageService = imageService;
            _selectedStoresService = selectedStoresService;
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
            var articleViewModel = new ArticelViewModel
            {
                PublishBegin = DateTime.Today,
                PublishEnd = DateTime.Today
            };
            ViewData["CompanyId"] = new SelectList(_companyService.GetAll(), "CompanyId", "Name");
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "CategoryId", "Name");
            ViewData["StoreId"] = new SelectList(_storeService.GetAll(), "StoreId", "Name");
            return View(articleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticelViewModel model)
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

                    var newArticle = Mapper.ViewModelToModelMapping.EditActicleViewModelToArticle(model);
                 
                    _articelService.Add(newArticle);

                    newArticle.ArticleCategory.Add(new ArticleCategory
                    {
                        ArticleId = newArticle.ArticleId,                        
                        CategoryId = categoryId,                 
                    });
                  
                    foreach (var storeId in selectedStoreListIds)
                    {
                        newArticle.StoreArticle.Add(new StoreArticle
                        {
                            ArticleId = newArticle.ArticleId,
                            StoreId = storeId
                        });
                    }
                    
                    var imageDirectoryPath = _imageService.CreateImageDirectory(newArticle);

                    var imageFile = model.ImageFile;
                    var saveImageToPath = _imageService.SaveImageToPath(newArticle, imageDirectoryPath, imageFile);

                    newArticle.ImageWidths = _imageService.CreateResizeImagesToImageDirectory(imageFile, saveImageToPath, imageDirectoryPath);

                    _articelService.Update(newArticle);
                    _imageService.TryToDeleteOriginalImage(saveImageToPath);
                }
            }

            ViewData["CompanyId"] = new SelectList(_companyService.GetAll(), "CompanyId", "Company");
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "CategoryId", "Category");
            ViewData["StoreId"] = new SelectList(_storeService.GetAll(), "StoreId", "Store");
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var article = _articelService.Find(id, "ArticleCategory.Category", "StoreArticle.Store");
            var test = article.ArticleCategory;
            var model = Mapper.ModelToViewModelMapping.ArticleToArticleViewModel(article);

            ViewBag.categoryNames = model.ArticleCategory.Select(x => x.Category.Name);
            ViewBag.storeNames = model.StoreArticle.Select(x => x.Store.Name);
            var categoryName = model.ArticleCategory.Select(x => x.Category.Name);

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
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "CategoryId", "Name");
            ViewData["StoreId"] = new SelectList(_storeService.GetAll(), "StoreId", "Name", stores);
            ViewData["CompanyId"] = new SelectList(_companyService.GetAll(), "CompanyId", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticelViewModel model)
        {
            if (ModelState.IsValid)
            {
                var article = _articelService.Find(model.ArticleId, "ArticleCategory.Category", "StoreArticle.Store");

                //if (model.ImageFile == null)//Image service? //Onödig?
                //{
                //    model.ImageFileFormat = article.ImageFileFormat;
                //    model.ImageFileName = article.ImageFileName;
                //    model.ImagePath = article.ImagePath;
                //    model.ImageWidths = article.ImageWidths;
                //}


                foreach (var storeId in model.StoreIds)
                {
                    model.StoreArticle.Add(new StoreArticle
                    {
                        StoreId = storeId,
                        ArticleId = model.ArticleId
                    });
                }
                foreach (var item in model.StoreArticle)//Storeart service refactor
                {
                    _storeArticleService.Update(item);
                }
                
                var categoriesId = article.ArticleCategory.Select(x => x.CategoryId);

                model.ArticleCategory.Add(new ArticleCategory
                {
                    ArticleId = model.ArticleId,
                    CategoryId = model.CategoryId
                });
                foreach (var categoryId in categoriesId)// Artcat serive refactor
                {

                    if (categoryId != model.CategoryId)
                    {
                        model.ArticleCategory.Add(new ArticleCategory
                        {
                            ArticleId = model.ArticleId,
                            CategoryId = model.CategoryId,

                        });
                    }
                    else
                    {
                        model.ArticleCategory = article.ArticleCategory;
                    }
                }
                model.Slug = _articelService.GenerateSlug(model.Name);
                model.UserId = article.UserId;
                if (model.ImageFile != null)
                {
                    _imageService.DeleteAllOldImages(article.ImagePath, article.ImageFileName);
                }
                article = Mapper.ViewModelToModelMapping.EditActicleViewModelToArticle(model, article);

                if (model.ImageFile != null)
                {                    
                    var imageDirectoryPath = _imageService.CreateImageDirectory(article);

                    var imageFile = model.ImageFile;
                    var saveImageToPath = _imageService.SaveImageToPath(article, imageDirectoryPath, imageFile);

                    article.ImageWidths = _imageService.CreateResizeImagesToImageDirectory(imageFile, saveImageToPath, imageDirectoryPath);

                   
                    _imageService.TryToDeleteOriginalImage(saveImageToPath);
                }
                
                _articelService.Update(article);
            }
            return RedirectToAction("Index");
        }
    }
}

//    ------Funktioner-------

// Litet problem med Kategori om den är tom ifrån början så går det inte att uppdatera den via Edit.
// Antagligen ge den en IdentityColumn också.
// 30min-60min

// Bygga en ny Viewmodel som är till för edit.
// 1 timma

// Validera datum
// 1-2 timmar

// Bygga till Image grejen på edit
// Ny image att lägga till. DONE
// Ta bort dom gamla.
// 1 timma

// --------Refactoring-----------
// Titta över namngivningen på allt igen(gå igenom hela flödet).*
// Rensa kommentarer ta bort servicar och dylikt som jag inte använder längre finns lite sånt.*
// Inte glömma att flytta styles och script från vyerna, till css och js filerna.(Create & Details)Sista grejen.
// Domain
// Data
// MVC
// 1 dag ungefär

//      --------Styling---------
// Stylingen skall påminna om den som redan finns på hemsidan får titta på den och se hur det ser ut.
// Styla Details sidan.
// Styla Edit sidan.
// Styla Create sidan.
//Osäker här gissar på 2-3 dagar

