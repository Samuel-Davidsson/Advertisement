using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnnonsonMVC.ViewModels;
using Domain.Interfaces;
using Domain.Entites;
using AnnonsonMVC.Utilities;
using System.Threading.Tasks;
using Data.Appsettings;
using Microsoft.Extensions.Options;

namespace AnnonsonMVC.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articelService;
        private readonly ICategoryService _categoryService;
        private readonly IStoreService _storeService;
        private readonly ICompanyService _companyService;
        private readonly IStoreArticleService _storeArticleService;
        private readonly ImageService _imageService;
        private readonly SelectedStoresService _selectedStoresService;
        private readonly AppSettings _appSettings;

        public ArticlesController(IArticleService articleService, ICategoryService categoryService, IStoreService storeService, ICompanyService companyService, 
            IStoreArticleService storeArticleService, ImageService imageService, 
            SelectedStoresService selectedStoresService, IOptions<AppSettings> appSettings)
        {
            _articelService = articleService;
            _categoryService = categoryService;
            _storeService = storeService;
            _companyService = companyService;
            _storeArticleService = storeArticleService;
            _imageService = imageService;
            _selectedStoresService = selectedStoresService;
            _appSettings = appSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _articelService.GetAll();
            var userArticles = articles.Where(x => x.UserId == 3);
            return View(userArticles.ToList());
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CompanyId"] = new SelectList(await _companyService.GetAll(), "CompanyId", "Name");
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAll(), "CategoryId", "Name");
            ViewData["StoreId"] = new SelectList(await _storeService.GetAll(), "StoreId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticelViewModel model)
        {

            if (ModelState.IsValid)
            {
                var stores = await _storeService.GetAll();
                if (model.StoreIds != null)
                {                    
                    var tempSlug = model.Name;
                    model.Slug = _articelService.GenerateSlug(tempSlug);
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

            ViewData["CompanyId"] = new SelectList(await _companyService.GetAll(), "CompanyId", "Company");
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAll(), "CategoryId", "Category");
            ViewData["StoreId"] = new SelectList(await _storeService.GetAll(), "StoreId", "Store");
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

        public async Task<IActionResult> Edit(int id)
        {     
            var article = _articelService.Find(id, "StoreArticle.Store", "ArticleCategory.Category");
            var companyId = article.CompanyId;
            
            var articleCategory = article.ArticleCategory.FirstOrDefault(x => x.ArticleId == article.ArticleId);

            var model = Mapper.ModelToViewModelMapping.ArticleToArticleViewModel(article);
           
            model.CategoryId = articleCategory.CategoryId;
            var stores = model.StoreArticle.Select(x => x.StoreId).ToArray();
            model.StoreIds = stores;

            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAll(), "CategoryId", "Name");
            ViewData["StoreId"] = new SelectList(await _storeService.GetAll(), "StoreId", "Name");
            ViewData["CompanyId"] = new SelectList(await _companyService.GetAll(), "CompanyId", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticelViewModel model)
        {
            if (ModelState.IsValid)
            {
                var article = _articelService.Find(model.ArticleId, "StoreArticle.Store", "ArticleCategory.Category");

                if (model.ImageFile == null)
                {
                    model.ImageFileFormat = article.ImageFileFormat;
                    model.ImageFileName = article.ImageFileName;
                    model.ImagePath = article.ImagePath;
                    model.ImageWidths = article.ImageWidths;
                }
                var tempSlug = model.Name;
                model.Slug = _articelService.GenerateSlug(tempSlug);
                model.UserId = article.UserId;

                //model.ArticleCategory.Add(new ArticleCategory
                //{
                //    ArticleId = model.ArticleId,
                //    CategoryId = model.CategoryId,
                //});

                //foreach (var storeId in model.StoreIds)
                //{
                //    model.StoreArticle.Add(new StoreArticle
                //    {
                //        ArticleId = model.ArticleId,
                //        StoreId = storeId
                //    });
                //}

                article = Mapper.ViewModelToModelMapping.EditActicleViewModelToArticle(model, article);
                _articelService.Update(article);
            }
            return RedirectToAction("Index");
        }
    }
}

//    ------Funktioner-------

// Deleta alla gamla bilder om man lägger till en ny.(vänta med)
// Man skall se orginalet först väljer man ny bild så byts den ut bara man kan INTE ändra tillbaka då får man gå tillbaka(vänta med)
// Bygga en ny Viewmodel som är till för edit.(idag)

    // Problem om man inte ändrar kategori eller storeart t.ex då blir det error eftersom den redan finns måste ha nån check på detta.

// Fixa till dom som är single value det är inte snyggt måste kunna göra det på ett bättre sätt? Funkar i alla fall.

// --------Refactoring-----------

// Titta över namngivningen på allt igen(gå igenom hela flödet).*
// Rensa kommentarer ta bort servicar och dylikt som jag inte använder längre finns lite sånt.*
// Inte glömma att flytta styles och script från vyerna, till css och js filerna.(Create & Details)Sista grejen.


//      --------Styling---------
// Stylingen skall påminna om den som redan finns på hemsidan får titta på den och se hur det ser ut(Kommer ta lite tid).
// Styla Details sidan.
// Styla Edit sidan.
// Styla Create sidan.


//   ----------Frågor-----------

// Angående strukturen på mitt projekt(Speciellt funktionerna i utilitys om dom skall vara där)?
// Appsettings rätt gjort/tänkt?
