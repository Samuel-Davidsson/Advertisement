using AnnonsonMVC.ViewModels;
using Domain.Entites;

namespace AnnonsonMVC.Utilities
{
    internal class ViewModelToModelMapper
    {

        public Article EditActicleViewModelToArticle(ArticleEditViewModel model, Article article)
        {
            article.Name = model.Name;
            article.Description = model.Description;
            article.Price = model.Price;
            article.PriceText = model.PriceText;
            article.PriceUnit = model.PriceUnit;
            article.Slug = model.Slug;
            article.ImagePath = model.ImagePath;
            article.ImageFileFormat = model.ImageFileFormat;
            article.ImageFileName = model.ImageFileName;
            article.ImageWidths = model.ImageWidths;
            article.PublishBegin = model.PublishBegin;
            article.PublishEnd = model.PublishEnd;
            article.CompanyId = model.CompanyId;
            article.UserId = model.UserId;
            article.ArticleCategory = model.ArticleCategory;
            article.StoreArticle = model.StoreArticle;
            return article;

    }

        public Article EditActicleViewModelToArticle(ArticelViewModel model)
        {
            return new Article
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                PriceText = model.PriceText,
                PriceUnit = model.PriceUnit,
                Slug = model.Slug,
                ImagePath = model.ImagePath,
                ImageFileFormat = model.ImageFileFormat,
                ImageFileName = model.ImageFileName,
                ImageWidths = model.ImageWidths,
                PublishBegin = model.PublishBegin,
                PublishEnd = model.PublishEnd,
                CompanyId = model.Company.CompanyId,
                UserId = model.UserId,
                ArticleCategory = model.ArticleCategory,
                StoreArticle = model.StoreArticle
            };
        }
    }
}