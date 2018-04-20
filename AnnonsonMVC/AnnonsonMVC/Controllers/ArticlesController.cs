using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnnonsonMVC.ViewModels;
using Domain.Interfaces;
using Domain.Entites;
using Microsoft.AspNetCore.Hosting;
using AnnonsonMVC.Utilities;
using System.Threading.Tasks;

namespace AnnonsonMVC.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articelService;
        private readonly ICategoryService _categoryService;
        private readonly IStoreService _storeService;
        private readonly ICompanyService _companyService;
        private readonly IStoreArticleService _storeArticleService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ImageService _imageService;
        private readonly SelectedStoresService _selectedStoresService;

        public ArticlesController(IArticleService articleService, ICategoryService categoryService, IStoreService storeService, ICompanyService companyService, 
            IHostingEnvironment hostingEnvironment,  IStoreArticleService storeArticleService, ImageService imageService, SelectedStoresService selectedStoresService)
        {
            _articelService = articleService;
            _categoryService = categoryService;
            _storeService = storeService;
            _companyService = companyService;
            _storeArticleService = storeArticleService;
            _hostingEnvironment = hostingEnvironment;
            _imageService = imageService;
            _selectedStoresService = selectedStoresService;
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

                    newArticle.Company.CompanyId = newArticle.CompanyId;

                    foreach (var storeId in selectedStoreListIds)
                    {
                        newArticle.StoreArticle.Add(new StoreArticle
                        {
                            ArticleId = newArticle.ArticleId,
                            StoreId = storeId
                        });
                    }
                    
                    var imageDirectoryPath = _articelService.CreateImageDirectory(newArticle);

                    var imagepath = _imageService.ImagePath(newArticle, imageDirectoryPath, model);

                    _imageService.CreateResizeImagesToImageDirectory(model, imagepath);

                    _articelService.Update(newArticle);
                    _imageService.TryToDeleteOriginalImage(imagepath);
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

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }
    }
  }

// Vad är kvar?


//    ------Funktioner-------
// Använda rätt path för image <appsettings>(Fredag).
// Ladda upp bilden till details & edit.(Börja titta på detta imorgon Fredag om tid finns annars Måndag).

// User delen inlogg? Fråga Fredrik här.(eventuellt ta det under helgen).
// Widths kvar kan inte göra en stringbuilder här göra det i utilitys och importa hit? kan inte göra det eftersom encodern inte gillar det.(ImageWidths hårdkodad for now).



//      --------Styling---------
// Snygga till knappar istället för länkar.(verkligen börja titta på detta under helgen imorgon eller nästa vecka) 
// Ändra ordningen på allt på create sidan.(Helgen eller imorgon eventuellt måndag)
// Börja titta på Details, och Edit sidan också.(nästa vecka)

