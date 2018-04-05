using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnnonsonMVC.ViewModels;
using Domain.Interfaces;
using System;
using Domain.Entites;
using AnnonsonMVC.Utilitys;

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

                model.UserId = 2;


                var newArticle = Mapper.ViewModelToModelMapping.EditActicleViewModelToArticle(model);
                _articelService.Add(newArticle);
                
                var imageName = "aid" + newArticle.ArticleId + "-" + Guid.NewGuid();
                newArticle.ImageFileName = imageName;

                _articelService.Update(newArticle);

                return View();
            }
            //ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "Company");
            //ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Category");
            return View();
        }
      }
    }

