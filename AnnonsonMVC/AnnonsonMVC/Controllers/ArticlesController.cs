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

;
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
            var article = _articelService.Find(id);
            var model = Mapper.ModelToViewModelMapping.ArticleToArticleViewModel(article);

            ViewBag.MediaUrl = _appSettings.MediaUrl;
            if (article == null)
            {
                return NotFound();
            }

            return View();
        }

        public IActionResult Edit(int id)
        {
            var article = _articelService.Find(id);
            var model = Mapper.ModelToViewModelMapping.ArticleToArticleViewModel(article);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        public IActionResult Edit(ArticelViewModel model)
        {
            if (ModelState.IsValid)
            {
                var article = _articelService.Find(model.ArticleId);
                article = Mapper.ViewModelToModelMapping.EditActicleViewModelToArticle(article, model);
                _articelService.Update(article);

            }

            return Redirect("Index");
        }
     }
  }


//    ------Funktioner-------
// Logiken för att visa bilden på details är nästan klar måste ha någon slags check om en bild inte finns i storlek skall den ta nästa efter det,
// förmodligen göra det i controllern och sen refactorisera ut det är nog det mesta alternativet.
// Fixa så att jag kan mappa till edit func och också ändra på något och spara det halvfärdig.


// --------Refactoring-----------

// Flytta tillbaka Sluggen till utilitys hör hemma där mer än vad den gör i articleservice Osäker antar att jag kan göra det.(Idag)
// Titta över namngivningen på allt igen(gå igenom hela flödet).*
// Rensa kommentarer ta bort servicar och dylikt som jag inte använder längre finns lite sånt.*
// Inte glömma att flytta styles och script från vyerna, till css och js filerna.(Create & Details)Sista grejen.






// Att göra idag:
// Styla Details sidan(Får göra om hela sidan där den ser förjävlig ut)
// Refactorisera gå igenom hela flödet alla klasser(Bara ta bort sånt som inte används)
// Edit fixa funktionalitet bakom den.



//      --------Styling---------
// Styla Details sidan*
// Styla Edit sidan


//   ----------Frågor att ställa!-----------

// User inlogg?
// Image loop i details? Så att mindre bilder syns om 512 inte finns.


// Angående strukturen på mitt projekt(om allt ligger hyfsat rätt)?
// Appsettings rätt gjort/tänkt?
