using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnnonsonMVC.ViewModels;
using Domain.Interfaces;
using System;
using AnnonsonMVC.Utilitys;
using Domain.Entites;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ImageMagick;

namespace AnnonsonMVC.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articelService;
        private readonly ICategoryService _categoryService;
        private readonly IStoreService _storeService;
        private readonly ICompanyService _companyService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ArticlesController(IArticleService articleService, ICategoryService categoryService, IStoreService storeService, ICompanyService companyService, IHostingEnvironment hostingEnvironment  )
        {
            _articelService = articleService;
            _categoryService = categoryService;
            _storeService = storeService;
            _companyService = companyService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var articles = _articelService.GetAll().Where(t => t.UserId == 3);
            return View( articles.ToList());
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return View();
        }
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_companyService.GetAll(), "CompanyId", "Name");
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "CategoryId", "Name");
            ViewData["StoreId"] = new SelectList(_storeService.GetAll(), "StoreId", "Name");
            //StoreArticle service istället för att hämta flera stores?
            //Byta typ av lista också.
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticelViewModel model)
        {

            if (ModelState.IsValid)
            {

              var slug = model.Name.Replace(" ", "-").ToLower();
                model.Slug = slug;

                model.UserId = 3;

                //Placeholders since we dont have ArticleID until it´s saved.
                //Also needed to make articleCategory cause it didnt stick onto the model.
                var storeId = model.Store.StoreArticle;
                var categoryId = model.Category.CategoryId;
                var articleCategory = model.Category.ArticleCategory;
                var storeart = model.Store.StoreArticle;

                var newArticle = Mapper.ViewModelToModelMapping.EditActicleViewModelToArticle(model);
                _articelService.Add(newArticle);

                newArticle.ArticleCategory = articleCategory;
                newArticle.StoreArticle = storeart;

                if (model.ImageFile == null || model.ImageFile.Length == 0)
                    return Content("file not selected");

                newArticle.ImageFileName = "aid" + newArticle.ArticleId + "-" + Guid.NewGuid();

                var uploadpath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");
                var filepath = Path.Combine(uploadpath, "aid" + newArticle.ArticleId + "-" + Guid.NewGuid() + ".jpg").Trim('"');
                
                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(filestream);
                }

                using (var image = new MagickImage(filepath))
                {
                    image.Resize(600, 600);
                    image.Strip();
                    image.Settings.FillColor = MagickColors.Green;
                    image.Write(filepath);
                }

                foreach (var storearticle in newArticle.StoreArticle)
                {
                    newArticle.StoreArticle.Add(new StoreArticle
                    {
                        ArticleId = newArticle.ArticleId,
                        StoreId = storearticle.StoreId
                    });
                }
               
                newArticle.ArticleCategory.Add(new ArticleCategory
                {
                    ArticleId = newArticle.ArticleId,
                    CategoryId = categoryId,
                });

                _articelService.Update(newArticle);
                
                return View();
            }
            ViewData["CompanyId"] = new SelectList(_companyService.GetAll(), "CompanyId", "Company");
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "CategoryId", "Category");
            return View();


            //Vad är kvar?

            // Directory skapa ny mapp med hjälp av datum och en check om det redan finns en mapp
            // Lista för storeartikel så man kan selecta massa.
            // Image Preview
            // Image format width height
            // Fylla ut image om den är för liten
            // User delen inlogg och annat? Fråga till Fredrik här
            // Använda rät path för image <appsettings>
            // Snygga till knappar istället för länkar

        }
      }
    }

