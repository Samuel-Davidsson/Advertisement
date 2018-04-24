using AnnonsonMVC.ViewModels;
using Domain.Entites;

namespace AnnonsonMVC.Utilities
{
    internal class ModelToViewModelMapper
    {
        public ModelToViewModelMapper()
        {

        }

        public ArticelViewModel ArticleToEditUserViewModel(Article article)
        {
            return new ArticelViewModel
            {
                Name = article.Name,
                Description = article.Description,
                Price = article.Price,
                PriceText = article.PriceText,
                PriceUnit = article.PriceUnit,
                Slug = article.Slug,
                ImagePath = article.ImagePath,
                ImageFileFormat = article.ImageFileFormat,
                ImageFileName = article.ImageFileName,
                ImageWidths = article.ImageWidths,
                PublishBegin = article.PublishBegin,
                PublishEnd = article.PublishEnd,
                Company = article.Company,
                UserId = article.UserId,
                ArticleCategory = article.ArticleCategory
            };
        }

        public ArticelViewModel ArticleToArticleViewModel(Article article)
        {
            return new ArticelViewModel
            {
                Name = article.Name,
                Description = article.Description,
                Price = article.Price,
                PriceText = article.PriceText,
                PriceUnit = article.PriceUnit,
                ImagePath = article.ImagePath,
                ImageFileFormat = article.ImageFileFormat,
                ImageFileName = article.ImageFileName,
                ImageWidths = article.ImageWidths,
                PublishBegin = article.PublishBegin,
                PublishEnd = article.PublishEnd,
            };
        }
    }
}