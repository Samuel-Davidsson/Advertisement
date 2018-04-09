using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnnonsonMVC.ViewModels;
using Domain.Interfaces;
using System;
using AnnonsonMVC.Utilitys;
using Domain.Entites;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AnnonsonMVC.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articelService;
        private readonly ICategoryService _categoryService;
        private readonly IStoreService _storeService;
        private readonly ICompanyService _companyService;

        public ArticlesController(IArticleService articleService, ICategoryService categoryService, IStoreService storeService, ICompanyService companyService)
        {
            _articelService = articleService;
            _categoryService = categoryService;
            _storeService = storeService;
            _companyService = companyService;
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

        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_companyService.GetAll(), "CompanyId", "Name");
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "CategoryId", "Name");
            ViewData["StoreId"] = new SelectList(_storeService.GetAll(), "StoreId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticelViewModel model, IFormFile File)
        {

            if (ModelState.IsValid)
            {
                //model.ImagePath nån special path.
                //model.ImageFileFormat fixa till filformatet inte så svårt.
                //model.ImageWidths fixa till filformatet inte så svårt heller
                //model.ImageFileName redan klar

                var slug = model.Name.Replace(" ", "-").ToLower();
                model.Slug = slug;

                model.UserId = 2; //No login yet.

                //Placeholders since we dont have ArticleID until it´s saved.
                //Also needed to make articleCategory cause I didnt stick onto the model.
                var storeId = model.Store.StoreId;
                var categoryId = model.Category.CategoryId;
                var articleCategory = model.Category.ArticleCategory;
                var articleStore = model.Store.StoreArticle;

                var newArticle = Mapper.ViewModelToModelMapping.EditActicleViewModelToArticle(model);
                _articelService.Add(newArticle);

                newArticle.ArticleCategory = articleCategory;
                newArticle.StoreArticle = articleStore;

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

