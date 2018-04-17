using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnnonsonMVC.ViewModels;
using Domain.Interfaces;
using System;
using Domain.Entites;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Collections.Generic;
using AnnonsonMVC.Utilities;
using System.Drawing;
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
                    var selectedStores = stores.Select(x => new SelectListItem { Value = x.StoreId.ToString(), Text = x.Name }).ToList();
                    model.Stores = selectedStores;
                    List<SelectListItem> selectedStoreList = model.Stores.Where(x => model.StoreIds.Contains(int.Parse(x.Value))).ToList();

                    foreach (var selecteditem in selectedStoreList)
                    {
                        selecteditem.Selected = true;
                        //ViewBag.message += "\\n" + selecteditem.Text;
                    }

                    var selectedStoreListIds = selectedStoreList.Select(x => x.Value);
                    var selectedStoreListIdsToInt = selectedStoreListIds.Select(s => int.Parse(s)).ToList();

                    //Dela upp och göra metod här.
                    //Fungerar inte atm
                    var slug = model.Name.Replace(@"\s+", " ").Replace(@"[^a-z0-9\s-]", "").Trim().Replace(@"\s", "-").ToLower();
                    model.Slug = slug;
                    model.UserId = 2;
                    var categoryId = model.CategoryId;
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

                    newArticle.ImageFileName = "aid" + newArticle.ArticleId + "-" + Guid.NewGuid();
                    string year = DateTime.Now.ToString("yyyy");
                    string month = DateTime.Now.ToString("MM");
                    string day = DateTime.Now.ToString("dd");
                    var todaysDate = year + @"\" + month + @"\" + day;
                    var uploadpath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads\\" + todaysDate);

                    if (!Directory.Exists(uploadpath))
                    {
                        Directory.CreateDirectory(uploadpath);
                    }

                    var imagepath = Path.Combine(uploadpath, "aid" + newArticle.ArticleId + "-" + Guid.NewGuid()).Replace(@"\\", @"\");

                    using (var imagestream = new FileStream(imagepath, FileMode.Create))
                    {
                        model.ImageFile.CopyTo(imagestream);

                    }

                    Stream stream = model.ImageFile.OpenReadStream();
                    Image resizeImage = Image.FromStream(stream);

                    if (resizeImage.Width >= 2048)
                    {
                        resizeImage = new Bitmap(2048, 2048);
                        resizeImage.Save(imagepath + "-2048.jpg");
                    }
                    if (resizeImage.Width >= 1024)
                    {
                        resizeImage = new Bitmap(1024, 1024);
                        resizeImage.Save(imagepath + "-1024.jpg");
                    }
                    if (resizeImage.Width >= 512)
                    {
                        resizeImage = new Bitmap(512, 512);
                        resizeImage.Save(imagepath + "-512.jpg");
                    }
                    if (resizeImage.Width >= 128)
                    {
                        resizeImage = new Bitmap(128, 128);
                        resizeImage.Save(imagepath + "-128.jpg");
                    }
                    if (resizeImage.Width == 0)
                    {

                    }

                    newArticle.ImagePath = todaysDate;
                    _articelService.Update(newArticle);
                }
            }
            
            //ViewData["CompanyId"] = new SelectList(_companyService.GetAll().ToString(), "CompanyId", "Company");
            //ViewData["CategoryId"] = new SelectList(_categoryService.GetAll().ToString(), "CategoryId", "Category");
            //ViewData["StoreId"] = new SelectList(_storeService.GetAll().ToString(), "StoreId", "Store");
            return View();
            //return View(model);
            //Vad fan skall jag returna här.


            // Vad är kvar?
            // Tänka om lite när det gäller pathen
            // Image format(Width, Height)
            // 4 Olika format skall bilden sparas i 4 olika format i olika mappar(imorgon tror jag).
            // Måste göra om till async

            // Använda rätt path för image <appsettings> Lätt tror jag.
            // User delen inlogg? Fråga Fredrik här.

            //      -------Styling--------
            // Fixa till multiple selectlistan
            // Snygga till knappar istället för länkar
            // Annotations meddelanden när man missat att fylla i någonting.
            // Refactor Controllern.

        }
    }
  }

