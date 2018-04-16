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
using System.Collections.Generic;

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
                var stores = _storeService.GetAll();
                if (model.StoreIds != null)
                {
                    var selectedStores = stores.Select(x => new SelectListItem { Value = x.StoreId.ToString(), Text = x.Name }).ToList();
                    model.Stores = selectedStores;
                    List<SelectListItem> selectedStoreList = model.Stores.Where(x => model.StoreIds.Contains(int.Parse(x.Value))).ToList();

                    foreach (var selecteditem in selectedStoreList)
                    {
                        selecteditem.Selected = true;
                        ViewBag.message += "\\n" + selecteditem.Text;
                    }

                    var selectedStoreListIds = selectedStoreList.Select(x => x.Value);
                    var selectedStoreListIdsToInt = selectedStoreListIds.Select(s => int.Parse(s)).ToList();


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

                    foreach (var storeId in selectedStoreListIdsToInt)
                    {
                        newArticle.StoreArticle.Add(new StoreArticle
                        {
                            ArticleId = newArticle.ArticleId,
                            StoreId = storeId
                        });
                    }

                if (model.ImageFile == null || model.ImageFile.Length == 0)
                    return Content("Ingen bild är uppladdad");

                newArticle.ImageFileName = "aid" + newArticle.ArticleId + "-" + Guid.NewGuid();
                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                    var todaysDate = date.Replace("-", @"\");
                    var uploadpath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads\\" + todaysDate);

                    if (!Directory.Exists("\\wwwroot\\Uploads\\" +todaysDate))
                    {
                        Directory.CreateDirectory("\\wwwroot\\Uploads\\" + todaysDate);
                    }
                    //Här skall vi resiza.
                    var filepath = Path.Combine(uploadpath, "aid" + newArticle.ArticleId + "-" + Guid.NewGuid() + ".jpg").Replace(@"\\", @"\");

                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(filestream);
                }


                    newArticle.ImagePath = todaysDate;//Detta är kanske okej.
                _articelService.Update(newArticle);

                return View();
            }
            }

            ViewData["CompanyId"] = new SelectList(_companyService.GetAll(), "CompanyId", "Company");
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "CategoryId", "Category");
            ViewData["StoreId"] = new SelectList(_storeService.GetAll(), "StoreId", "Store");
            return View();


            // Vad är kvar?

            // Image format(Width, Height)
            // 4 Olika format skall bilden sparas i 4 olika format i olika mappar(imorgon tror jag).

            // Använda rätt path för image <appsettings> Lätt tror jag.
            // User delen inlogg och annat? Fråga Fredrik här.

            //      -------Styling--------
            // Fixa till multiple selectlistan
            // Snygga till knappar istället för länkar
            // Annotations meddelanden när man missat att fylla i någonting.

        }
    }
  }

