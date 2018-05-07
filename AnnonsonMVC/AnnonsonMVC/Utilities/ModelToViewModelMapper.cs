using AnnonsonMVC.ViewModels;
using Domain.Entites;

namespace AnnonsonMVC.Utilities
{
    internal class ModelToViewModelMapper
    {
        public ModelToViewModelMapper()
        {

        }

        public ArticelViewModel ArticleToArticleViewModel(Article article)
        {
            return new ArticelViewModel
            { 
                ArticleId = article.ArticleId,
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
                ArticleCategory = article.ArticleCategory,
                StoreArticle = article.StoreArticle,
                Company = article.Company,
                UserId = article.UserId,
                CompanyId = article.CompanyId,
                Slug = article.Slug,               
            };
        }
    }
}