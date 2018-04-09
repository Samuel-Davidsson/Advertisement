using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnnonsonMVC.ViewModels;
using Domain.Interfaces;
using System;
using AnnonsonMVC.Utilitys;
using Domain.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticelViewModel model, IFormFile ImageFile)
        {

            if (ModelState.IsValid)
            {
                if (ImageFile == null || ImageFile.Length == 0)
                    return Content("file not selected");

                //Tar sig bara till wwwroot.
                var upload = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");
                var filepath = Path.Combine(upload, ImageFile.FileName);
                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    ImageFile.CopyTo(filestream);
                }

                //model.ImageFileFormat fixa till filformatet svårare än jag trodde får googla detta.
                //model.ImageWidths fixa till filformatet samma hät svårare än jag trodde.
                //Pathen går bara in i projektet måste få den att gå utanför på något sätt.

                var slug = model.Name.Replace(" ", "-").ToLower();
                model.Slug = slug;

                //No login yet.
                model.UserId = 3;

                //Placeholders since we dont have ArticleID until it´s saved.
                //Also needed to make articleCategory cause it didnt stick onto the model.
                var storeId = model.Store.StoreId;
                var categoryId = model.Category.CategoryId;
                var articleCategory = model.Category.ArticleCategory;

                var newArticle = Mapper.ViewModelToModelMapping.EditActicleViewModelToArticle(model);
                _articelService.Add(newArticle);

                newArticle.ArticleCategory = articleCategory;

                //Måste göra en lista av detta man skall kunna välja flera.
                newArticle.StoreArticle.Add(new StoreArticle
                {
                    ArticleId = newArticle.ArticleId,
                    StoreId = storeId
                });

                newArticle.ArticleCategory.Add(new ArticleCategory
                {
                    ArticleId = newArticle.ArticleId,
                    CategoryId = categoryId,
                });

                var imageName = "aid" + newArticle.ArticleId + "-" + Guid.NewGuid();
                newArticle.ImageFileName = imageName;

                _articelService.Update(newArticle);
                
                return View("Index");
            }
            ViewData["CompanyId"] = new SelectList(_companyService.GetAll(), "CompanyId", "Company");
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "CategoryId", "Category");
            return View();
        }
      }
    }

