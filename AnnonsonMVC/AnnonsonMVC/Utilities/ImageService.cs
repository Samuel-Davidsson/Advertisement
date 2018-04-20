using AnnonsonMVC.ViewModels;
using Data.Appsettings;
using Domain.Entites;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace AnnonsonMVC.Utilities
{
    public class ImageService
    {
        private readonly IHostingEnvironment _hostingEnvironment; //Får ligga kvar så länge
        private readonly AppSettings _appSettings;

        public ImageService(IHostingEnvironment hostingEnvironment, IOptions<AppSettings> appSettings)
        {
            _hostingEnvironment = hostingEnvironment;
            _appSettings = appSettings.Value;
        }

        public string SaveImageToPath(Article newArticle, string imageDirectoryPath, ArticelViewModel model)
        {
            var saveImagePath = Path.Combine(imageDirectoryPath, newArticle.ImageFileName + ".jpg").Replace(@"\\", @"\");

            using (var imagestream = new FileStream(saveImagePath, FileMode.Create))
            {
                model.ImageFile.CopyTo(imagestream);
            }
            newArticle.ImagePath = DateTime.Now.ToString("yyy-MM-dd").Replace("-", @"\");
            newArticle.ImageWidths = "1024,512,256,128";
            newArticle.ImageFileFormat = "jpg";                          //Tveksam här...

            return saveImagePath;
        }

        public string CreateResizeImagesToImageDirectory(ArticelViewModel model, string SaveImagePath)
        {
            Stream imageStream = model.ImageFile.OpenReadStream();
            Image resizeImage = Image.FromStream(imageStream);
            var imageFileBitmapSize = resizeImage.Size;

            if (resizeImage.Width >= 2048)
            {
                resizeImage = MakeImageSquareAndFillBlancs(2048, 2048, imageFileBitmapSize, resizeImage, SaveImagePath);
                resizeImage.Save(SaveImagePath.Replace(".jpg", "") + "-2048.jpg");
            }

            if (resizeImage.Width >= 1024)
            {
                resizeImage = MakeImageSquareAndFillBlancs(1024, 1024, imageFileBitmapSize, resizeImage, SaveImagePath);
                resizeImage.Save(SaveImagePath.Replace(".jpg", "") + "-1024.jpg");
                model.ImageWidths = "1024, ";
            }
            if (resizeImage.Width >= 512)
            {
                resizeImage = MakeImageSquareAndFillBlancs(512, 512, imageFileBitmapSize, resizeImage, SaveImagePath);
                resizeImage.Save(SaveImagePath.Replace(".jpg", "") + "-512.jpg");
                model.ImageWidths = "512, ";
            }
            if (resizeImage.Width >= 256)
            {
                resizeImage = MakeImageSquareAndFillBlancs(256, 256, imageFileBitmapSize, resizeImage, SaveImagePath);
                resizeImage.Save(SaveImagePath.Replace(".jpg", "") + "-256.jpg");
                model.ImageWidths = "256, ";
            }
            if (resizeImage.Width >= 128)
            {
                var newImage = MakeImageSquareAndFillBlancs(128, 128, imageFileBitmapSize, resizeImage, SaveImagePath);
                newImage.Save(SaveImagePath.Replace("jpg", "") + "128.jpg");
                model.ImageWidths = "128, ";
            }
            resizeImage.Dispose();
            imageStream.Dispose();
            return model.ImageWidths;


        }

        public Image MakeImageSquareAndFillBlancs(int canvasWidth, int canvasHeight, Size imgFileBitmapSize, Image resizeImage, string saveImagePath)
        {
            var originalImage = new Bitmap(saveImagePath);

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
            graphic.DrawImage(originalImage, posX, posY, newWidth, newHeight);
            originalImage.Dispose();
            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
            EncoderParameters encoderParameters;
            encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

            return newImageSize;
        }

        public string CreateImageDirectory(Article newArticle)
        {
            newArticle.ImageFileName = "aid" + newArticle.ArticleId + "-" + Guid.NewGuid();
            string year = DateTime.Now.ToString("yyyy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");

            var todaysDate = year + @"\" + month + @"\" + day;
            var imageDirectoryPath = Path.Combine(_appSettings.MediaFolder + todaysDate).Trim();

            if (!Directory.Exists(imageDirectoryPath))
            {
                Directory.CreateDirectory(imageDirectoryPath);
            }

            return imageDirectoryPath;
        }

        public void TryToDeleteOriginalImage(string imagepath)
        {
            if (File.Exists(imagepath))
            {
                File.Delete(imagepath);
            }
            else
            {
                System.Console.WriteLine("Path doesnt exist");
            }
        }
    }
}
