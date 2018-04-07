using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnnonsonMVC.ViewModels;
using Domain.Interfaces;
using System;
using AnnonsonMVC.Utilitys;
using Domain.Entites;
using Data.DataContext;

namespace AnnonsonMVC.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articelService;
        private readonly ICategoryService _categoryService;
        private readonly IStoreService _storeService;
        private readonly ICompanyService _companyService;
        private readonly annonsappenContext _context;

        public ArticlesController(IArticleService articleService, ICategoryService categoryService, IStoreService storeService, ICompanyService companyService, annonsappenContext context)
        {
            _articelService = articleService;
            _categoryService = categoryService;
            _storeService = storeService;
            _companyService = companyService;
            _context = context;
        }

        public IActionResult Index()
        {
            var articles = _articelService.GetAll().Where(t => t.UserId == 2);
            return View( articles.ToList());
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
        public IActionResult Create(ArticelViewModel model)
        {
            if (ModelState.IsValid)
            {

                var slug = model.Name.Replace(" ", "-").ToLower();
                model.Slug = slug;

                model.UserId = 2; //No login yet.

                var storeId = model.Store.StoreId;          //Placeholders since we dont have ArticleID until it´s saved.
                var category = model.Category; //

                var newArticle = Mapper.ViewModelToModelMapping.EditActicleViewModelToArticle(model);
                _articelService.Add(newArticle);

                newArticle.StoreArticle.Add(new StoreArticle
                {
                    ArticleId = newArticle.ArticleId,
                    StoreId = storeId
                });

                //Fråga Freddan på Måndag. -->

                newArticle.ArticleCategory.Add(new ArticleCategory
                {
                    ArticleId = newArticle.ArticleId,
                    CategoryId = category.CategoryId,
                });

                var imageName = "aid" + newArticle.ArticleId + "-" + Guid.NewGuid();
                newArticle.ImageFileName = imageName;

                _articelService.Update(newArticle);

                return View();
            }
            ViewData["CompanyId"] = new SelectList(_companyService.GetAll(), "CompanyId", "Company");
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "CategoryId", "Category");
            return View();
        }
      }
    }

