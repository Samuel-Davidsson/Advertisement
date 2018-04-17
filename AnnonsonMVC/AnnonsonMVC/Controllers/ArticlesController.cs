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
                        System.Drawing.Imaging.ImageCodecInfo[] info = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
                        System.Drawing.Imaging.EncoderParameters encoderParameters;
                        encoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
                        encoderParameters.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

                        resizeImage.Save(imagepath.Replace(".jpg", "") + "-2048.jpg");
                    }
                    if (resizeImage.Width >= 1024)
                    {
                        resizeImage = new Bitmap(1024, 1024);
                        resizeImage.Save(imagepath.Replace(".jpg", "") + "-1024.jpg");
                    }
                    if (resizeImage.Width >= 512)
                    {
                        resizeImage = new Bitmap(512, 512);
                        resizeImage.Save(imagepath.Replace(".jpg", "") + "-512.jpg");
                    }
                    if (resizeImage.Width >= 256)
                    {
                        resizeImage = new Bitmap(256, 256);
                        resizeImage.Save(imagepath.Replace(".jpg", "") + "-256.jpg");
                    }
                    if (resizeImage.Width >= 128)
                    {
                        resizeImage = new Bitmap(128, 128);
                        resizeImage.Save(imagepath.Replace("jpg", "") + "128.jpg");
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
            // Image format(Width, Height)
            // 4 Olika format skall bilden sparas i 4 olika format i olika mappar(imorgon tror jag).

            // Använda rätt path för image <appsettings> Lätt tror jag.
            // User delen inlogg? Fråga Fredrik här.

            //      -------Styling--------
            // Fixa till multiple selectlistan
            // Snygga till knappar istället för länkar
            // Annotations meddelanden när man missat att fylla i någonting.
            // Refactor Controllern.

        }

        public Image MakeImageSquareAndFillBlancs(int canvasWidth, int canvasHeight, Size imgFileBitmapSize, Image resizeImage, string imagepath)
        {

            var image = new Bitmap(imagepath);

            int originalWidth = imgFileBitmapSize.Width;
            int originalHeight = imgFileBitmapSize.Height;

            //Define new picture size
            System.Drawing.Image newPicSize = new System.Drawing.Bitmap(canvasWidth, canvasHeight);
            System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(newPicSize);

            graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;


            // Figure out the ratio
            double ratioX = (double)canvasWidth / (double)resizeImage.Width;
            double ratioY = (double)canvasHeight / (double)resizeImage.Height;
            // use whichever multiplier is smaller
            double ratio = ratioX < ratioY ? ratioX : ratioY;

            // now we can get the new height and width
            int newHeight = Convert.ToInt32(resizeImage.Height * ratio);
            int newWidth = Convert.ToInt32(resizeImage.Width * ratio);

            // Now calculate the X,Y position of the upper-left corner
            // (one of these will always be zero)
            int posX = Convert.ToInt32((canvasWidth - (resizeImage.Width * ratio)) / 2);
            int posY = Convert.ToInt32((canvasHeight - (resizeImage.Height * ratio)) / 2);

            graphic.Clear(System.Drawing.Color.White); // white padding
            graphic.DrawImage(image, posX, posY, newWidth, newHeight);

            //System.Drawing.Imaging.ImageCodecInfo[] info = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            //System.Drawing.Imaging.EncoderParameters encoderParameters;
            //encoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
            //encoderParameters.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

            //var savedImagePathAndName = System.IO.Path.Combine("images", loadedFileName);


            //using (var fileStream = new FileStream(savedImagePathAndName, FileMode.Create))
            //{
            //    newPicSize.Save(fileStream, info[1], encoderParameters);
            //}

            return (newPicSize);
        }
    }
  }

