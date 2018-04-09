using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnnonsonMVC.ViewModels;
using Domain.Interfaces;
using System;
using AnnonsonMVC.Utilitys;
using Domain.Entites;
using Microsoft.AspNetCore.Http;

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
        public IActionResult Create(ArticelViewModel model, IFormFile ArticleImage)
        {
            if (ModelState.IsValid)
            {
                //var test = model.ArticleImage; 
                var slug = model.Name.Replace(" ", "-").ToLower();
                model.Slug = slug;

                model.UserId = 2; //No login yet.

                //Placeholders since we dont have ArticleID until it´s saved
                var storeId = model.Store.StoreId;
                var categoryId = model.Category.CategoryId;
                var articleCategory = model.Category.ArticleCategory;
                var articleStore = model.Store.StoreArticle;

                var newArticle = Mapper.ViewModelToModelMapping.EditActicleViewModelToArticle(model);
                _articelService.Add(newArticle);
                newArticle.ArticleCategory = articleCategory;


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

