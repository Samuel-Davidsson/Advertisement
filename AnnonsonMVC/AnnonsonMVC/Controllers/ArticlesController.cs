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
                    var selectedStoreListIds = GetSelectedStoresList(model, stores);
                    var tempSlug = model.Name;
                    model.Slug = GenerateSlug(tempSlug);

                    model.UserId = 3;

                    var categoryId = model.CategoryId;
                    var newArticle = Mapper.ViewModelToModelMapping.EditActicleViewModelToArticle(model);

                    _articelService.Add(newArticle);

                    newArticle.ArticleCategory.Add(new ArticleCategory
                    {
                        ArticleId = newArticle.ArticleId,
                        CategoryId = categoryId,                        
                    });

                    foreach (var storeId in selectedStoreListIds)
                    {
                        newArticle.StoreArticle.Add(new StoreArticle
                        {
                            ArticleId = newArticle.ArticleId,
                            StoreId = storeId
                        });
                    }

                    var imageDirectoryPath = CreateImageDirectory(newArticle);

                    var imagepath = ImagePath(newArticle, imageDirectoryPath, model);

                    CreateResizeImages(model, imagepath);
                    
                    _articelService.Update(newArticle);
                }
            }

            ViewData["CompanyId"] = new SelectList(await _companyService.GetAll(), "CompanyId", "Company");
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAll(), "CategoryId", "Category");
            ViewData["StoreId"] = new SelectList(await _storeService.GetAll(), "StoreId", "Store");
            return RedirectToAction("Index");
        }

        private string ImagePath(Article newArticle, string uploadpath, ArticelViewModel model)
        {
            var imagepath = Path.Combine(uploadpath, newArticle.ImageFileName + ".jpg").Replace(@"\\", @"\");

            using (var imagestream = new FileStream(imagepath, FileMode.Create))
            {
                model.ImageFile.CopyTo(imagestream);

            }
            newArticle.ImagePath = DateTime.Now.ToString("yyy-MM-dd").Replace("-", @"\");
            newArticle.ImageWidths = "1024,512,256,128";
            newArticle.ImageFileFormat = "jpg";                          //Tveksam här...

            return imagepath;
        }

        private string CreateImageDirectory(Article newArticle)
        {
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

            return uploadpath;
        }

        private string CreateResizeImages(ArticelViewModel model, string imagepath)
        {
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
                model.ImageWidths = "1024, ";
            }
            if (resizeImage.Width >= 512)
            {
                resizeImage = MakeImageSquareAndFillBlancs(512, 512, imgFileBitmapSize, resizeImage, imagepath);
                resizeImage.Save(imagepath.Replace(".jpg", "") + "-512.jpg");
                model.ImageWidths = "512, ";
            }
            if (resizeImage.Width >= 256)
            {
                resizeImage = MakeImageSquareAndFillBlancs(256, 256, imgFileBitmapSize, resizeImage, imagepath);
                resizeImage.Save(imagepath.Replace(".jpg", "") + "-256.jpg");
                model.ImageWidths = "256, ";
            }
            if (resizeImage.Width >= 128)
            {
                var newImage = MakeImageSquareAndFillBlancs(128, 128, imgFileBitmapSize, resizeImage, imagepath);
                newImage.Save(imagepath.Replace("jpg", "") + "128.jpg");
                model.ImageWidths = "128, ";
            }
            return model.ImageWidths;
            //stream.Dispose();

        }

        private string GenerateSlug(string tempSlug)
        {
            tempSlug = tempSlug.ToLower();
            tempSlug = Regex.Replace(tempSlug, "[äå]", "a");
            tempSlug = Regex.Replace(tempSlug, "[óòöôõø]", "o");
            tempSlug = Regex.Replace(tempSlug, "[úùüû]", "u");
            tempSlug = Regex.Replace(tempSlug, "[éèëê]", "e");
            tempSlug = Regex.Replace(tempSlug, @"\s", "-");
            tempSlug = Regex.Replace(tempSlug, @"\s+", " ").Trim();
            tempSlug = Regex.Replace(tempSlug, @"[^a-z0-9\s-]", "");
            return tempSlug;
        }

        private List<int> GetSelectedStoresList(ArticelViewModel model, IEnumerable<Store> stores)
        {
            var selectedStores = stores.Select(x => new SelectListItem { Value = x.StoreId.ToString(), Text = x.Name }).ToList();
            model.Stores = selectedStores;
            List<SelectListItem> selectedStoreList = model.Stores.Where(x => model.StoreIds.Contains(int.Parse(x.Value))).ToList();

            foreach (var selecteditem in selectedStoreList)
            {
                selecteditem.Selected = true;
            }

            var selectedStoreListIds = selectedStoreList.Select(x => x.Value).Select(x => int.Parse(x)).ToList();

            return selectedStoreListIds;
        }
               
        private Image MakeImageSquareAndFillBlancs(int canvasWidth, int canvasHeight, Size imgFileBitmapSize, Image resizeImage, string imagepath)
        {
            var image = new Bitmap(imagepath);

            int originalWidth = imgFileBitmapSize.Width;
            int originalHeight = imgFileBitmapSize.Height;

            Image newImageSize = new Bitmap(canvasWidth, canvasHeight);
            Graphics graphic = Graphics.FromImage(newImageSize);

            graphic.CompositingQuality = CompositingQuality.HighQuality;
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

            double ratioX = (double)canvasWidth / (double)resizeImage.Width;
            double ratioY = (double)canvasHeight / (double)resizeImage.Height;

            double ratio = ratioX < ratioY ? ratioX : ratioY;

            int newHeight = Convert.ToInt32(resizeImage.Height * ratio);
            int newWidth = Convert.ToInt32(resizeImage.Width * ratio);

            int posX = Convert.ToInt32((canvasWidth - (resizeImage.Width * ratio)) / 2);
            int posY = Convert.ToInt32((canvasHeight - (resizeImage.Height * ratio)) / 2);

            graphic.Clear(Color.White);
            graphic.DrawImage(image, posX, posY, newWidth, newHeight);

            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
            EncoderParameters encoderParameters;
            encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

            return newImageSize;
        }
    }
  }

// Vad är kvar?

// Använda rätt path för image <appsettings> Lätt tror jag.(idag)
// User delen inlogg? Fråga Fredrik här.(Fredag kanske eventuellt ta det under helgen).
// Widths kvar kan inte göra en stringbuilder här göra det i utilitys och importa hit? kan inte göra det eftersom encodern inte gillar det.
// Hårdkodat imageWidths..(osäker)
// Refactor flytta några saker till andra ställen.(osäker)
// Har en "main" bilden kvar också måste ta bort den.


//      -------Styling--------
// Fixa till multiple selectlistan.
// Snygga till knappar istället för länkar. 
// Annotations meddelanden när man missat att fylla i någonting.(fredag)

