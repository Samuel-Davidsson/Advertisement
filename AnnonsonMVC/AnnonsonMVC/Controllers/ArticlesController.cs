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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

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

                    var selectedStoreListIdsToInt = GenerateSelectedStoreList(model, stores);
                    GenerateSlug(model);//Refactor igen räcker nog att skicka in en sträng här.
                    
                    model.UserId = 3;//Detta är bara bas kan inte göra nåt åt denna atm.

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

                    var imagepath = Path.Combine(uploadpath, "aid" + newArticle.ArticleId + "-" + Guid.NewGuid() + ".jpg").Replace(@"\\", @"\");

                    using (var imagestream = new FileStream(imagepath, FileMode.Create))
                    {
                        model.ImageFile.CopyTo(imagestream);

                    }

                    Stream stream = model.ImageFile.OpenReadStream();
                    Image resizeImage = Image.FromStream(stream);

                    var imgFileBitmapSize = resizeImage.Size;
                    if (resizeImage.Width >= 2048)
                    {
                        resizeImage = MakeImageSquareAndFillBlancs(2048, 2048, imgFileBitmapSize, resizeImage, imagepath);
                        resizeImage.Save(imagepath.Replace(".jpg", "") + "-2048.jpg");
                    }
                    if (resizeImage.Width >= 1024)
                    {
                        resizeImage = MakeImageSquareAndFillBlancs(1024, 1024, imgFileBitmapSize, resizeImage, imagepath);
                        resizeImage.Save(imagepath.Replace(".jpg", "") + "-1024.jpg");
                    }
                    if (resizeImage.Width >= 512)
                    {
                        resizeImage = MakeImageSquareAndFillBlancs(512, 512, imgFileBitmapSize, resizeImage, imagepath);
                        resizeImage.Save(imagepath.Replace(".jpg", "") + "-512.jpg");
                    }
                    if (resizeImage.Width >= 256)
                    {
                        resizeImage = MakeImageSquareAndFillBlancs(256, 256, imgFileBitmapSize, resizeImage, imagepath);
                        resizeImage.Save(imagepath.Replace(".jpg", "") + "-256.jpg");
                    }
                    if (resizeImage.Width >= 128)
                    {
                        var newImage = MakeImageSquareAndFillBlancs(128, 128, imgFileBitmapSize, resizeImage, imagepath);
                        newImage.Save(imagepath.Replace("jpg", "") + "128.jpg");
                    }

                    newArticle.ImagePath = todaysDate;
                    _articelService.Update(newArticle);
                }
            }

            ViewData["CompanyId"] = new SelectList(await _companyService.GetAll(), "CompanyId", "Company");
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAll(), "CategoryId", "Category");
            ViewData["StoreId"] = new SelectList(await _storeService.GetAll(), "StoreId", "Store");
            return View();

            // Vad är kvar?

            // Använda rätt path för image <appsettings> Lätt tror jag.
            // User delen inlogg? Fråga Fredrik här.


            // ------Errors--------
            // Crashar ibland för att Category är null(listan).
            // Kan inte ta mig till andra sidor samma sak där articles är null på index när jag skall tillbaka.

            //      -------Styling--------
            // Fixa till multiple selectlistan
            // Snygga till knappar istället för länkar  
            // Annotations meddelanden när man missat att fylla i någonting.
            // Refactor Controllern.(Titta på vad jag skall lägga alla metoderna)
            // Path så jag kommer tillbaka till alla artiklar eller Details delen.

        }

        private string GenerateSlug(ArticelViewModel model)
        {
            model.Slug = model.Name;
            model.Slug = model.Name.Replace('Ö', 'O').Replace('Ä', 'A').Replace('Å', 'A').Replace('ö', 'o').Replace('ä', 'a').Replace('å', 'a').ToLower();
            model.Slug = Regex.Replace(model.Slug, @"[^a-z0-9\s-]", "");
            model.Slug = Regex.Replace(model.Slug, @"\s+", " ").Trim();
            model.Slug = Regex.Replace(model.Slug, @"\s", "-");
            
            return model.Slug;
        }

        private List<int> GenerateSelectedStoreList(ArticelViewModel model, IEnumerable<Store> stores)
        {
                var selectedStores = stores.Select(x => new SelectListItem { Value = x.StoreId.ToString(), Text = x.Name }).ToList();
                model.Stores = selectedStores;
                List<SelectListItem> selectedStoreList = model.Stores.Where(x => model.StoreIds.Contains(int.Parse(x.Value))).ToList();

                foreach (var selecteditem in selectedStoreList)
                {
                    selecteditem.Selected = true;
                }

                var selectedStoreListIds = selectedStoreList.Select(x => x.Value);
                var selectedStoreListIdsToInt = selectedStoreListIds.Select(s => int.Parse(s)).ToList();

                return selectedStoreListIdsToInt;
            }
               
        private Image MakeImageSquareAndFillBlancs(int canvasWidth, int canvasHeight, Size imgFileBitmapSize, Image resizeImage, string imagepath)
        {

            var image = new Bitmap(imagepath);

            int originalWidth = imgFileBitmapSize.Width;
            int originalHeight = imgFileBitmapSize.Height;

            Image newPicSize = new Bitmap(canvasWidth, canvasHeight);
            Graphics graphic = Graphics.FromImage(newPicSize);

            graphic.CompositingQuality = CompositingQuality.HighQuality;
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;


            // Figure out the ratio
            double ratioX = (double)canvasWidth / (double)resizeImage.Width;
            double ratioY = (double)canvasHeight / (double)resizeImage.Height;
            // use whichever multiplier is smaller
            double ratio = ratioX < ratioY ? ratioX : ratioY;

            int newHeight = Convert.ToInt32(resizeImage.Height * ratio);
            int newWidth = Convert.ToInt32(resizeImage.Width * ratio);

            // Now calculate the X,Y position of the upper-left corner
            // (one of these will always be zero)
            int posX = Convert.ToInt32((canvasWidth - (resizeImage.Width * ratio)) / 2);
            int posY = Convert.ToInt32((canvasHeight - (resizeImage.Height * ratio)) / 2);

            graphic.Clear(Color.White);
            graphic.DrawImage(image, posX, posY, newWidth, newHeight);

            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
            EncoderParameters encoderParameters;
            encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

            return newPicSize;
        }
    }
  }

