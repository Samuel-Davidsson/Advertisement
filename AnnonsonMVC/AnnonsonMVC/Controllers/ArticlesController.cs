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
using System.Collections.Generic;
using System.Collections;

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

        public ArticlesController(IArticleService articleService, ICategoryService categoryService, IStoreService storeService, ICompanyService companyService, 
            IHostingEnvironment hostingEnvironment,  IStoreArticleService storeArticleService)
        {
            _articelService = articleService;
            _categoryService = categoryService;
            _storeService = storeService;
            _companyService = companyService;
            _storeArticleService = storeArticleService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            
            var articles = _articelService.GetAll().Where(t => t.UserId == 3);
            return View( articles.ToList());

        }
    //    { Value = x.StoreId.ToString(), Text = x.Name
    //}).ToList();
        public IActionResult Create()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            ArticelViewModel model = new ArticelViewModel();
            var stores = _storeService.GetAll();

            stores.Select(x => x.UserStore.Where(y => y.UserId == 3));
            var selectedItems = stores.Select(x => new SelectListItem { Value = x.StoreId.ToString(), Text = x.Name }).ToList();
            //model.Stores = selectedItems;
            foreach (var storeId in stores)
            {
                var modelStoreId = model.Stores.Select(x => x.Value);
                var dbStoreId = stores.Select(x => x.StoreId);
                if (modelStoreId == dbStoreId)
                {
                    model.StoreIds[storeId];
                }
            }

            if (model.StoreIds != null)
            {
                List<SelectListItem> selectlistitems = model.Stores.Where(x => model.StoreIds.Contains(int.Parse(x.Value))).ToList();
                ViewBag.message("selcted stores");

                foreach (var selecteditem in selectlistitems)
                {
                    selecteditem.Selected = true;
                    ViewBag.message += "\\n" + selecteditem.Text;
                }
            }
            ViewData["CompanyId"] = new SelectList(_companyService.GetAll(), "CompanyId", "Name");
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "CategoryId", "Name");
                  
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

               




                var categoryId = model.Category.CategoryId;                
                var newArticle = Mapper.ViewModelToModelMapping.EditActicleViewModelToArticle(model);
                _articelService.Add(newArticle);

                newArticle.ArticleCategory.Add(new ArticleCategory
                {
                    ArticleId = newArticle.ArticleId,
                    CategoryId = categoryId,
                });






                                                                                                                    


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

                _articelService.Update(newArticle);
                
                return View();
            }
            ViewData["CompanyId"] = new SelectList(_companyService.GetAll(), "CompanyId", "Company");
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "CategoryId", "Category");
            ViewData["StoreId"] = new SelectList(_storeService.GetAll(), "StoreId", "Store");
            return View();


            //Vad är kvar?

            // Directory skapa ny mapp med hjälp av datum och en check om det redan finns en mapp
            // Lista för storeartikel så man kan selecta massa.
            // Image Preview
            // Image format width height
            // 4 Olika format skall bilden sparas i.
            // Fylla ut image om den är för liten
            // User delen inlogg och annat? Fråga till Fredrik här
            // Använda rätt path för image <appsettings>
            // Snygga till knappar istället för länkar

        }
      }
    }

