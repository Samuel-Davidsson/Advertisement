using AnnonsonMVC.ViewModels;
using Domain.Entites;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace AnnonsonMVC.Utilities
{
    public class ImageService
    {
        public string ImagePath(Article newArticle, string uploadpath, ArticelViewModel model)
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

        public string CreateResizeImagesToImageDirectory(ArticelViewModel model, string imagepath)
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
            resizeImage.Dispose();
            stream.Dispose();
            return model.ImageWidths;


        }

        public Image MakeImageSquareAndFillBlancs(int canvasWidth, int canvasHeight, Size imgFileBitmapSize, Image resizeImage, string imagepath)
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
            image.Dispose();
            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
            EncoderParameters encoderParameters;
            encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

            return newImageSize;
        }

        public void TryToDeleteOriginalImage(string imagepath)
        {
            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }
            else
            {
                System.Console.WriteLine("Path doesnt exist");
            }

        }
    }
}
