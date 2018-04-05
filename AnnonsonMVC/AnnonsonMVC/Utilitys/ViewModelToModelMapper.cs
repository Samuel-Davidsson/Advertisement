using System;
using AnnonsonMVC.ViewModels;
using Domain.Entites;

namespace AnnonsonMVC.Utilitys
{
    internal class ViewModelToModelMapper
    {

        public Article EditActicleViewModelToArticle(Article articleToEdit, ArticelViewModel model)
        {
            articleToEdit.Name = model.Name;
            articleToEdit.Description = model.Description;
            articleToEdit.Price = model.Price;
            articleToEdit.PriceText = model.PriceText;
            articleToEdit.PriceUnit = model.PriceUnit;
            articleToEdit.Slug = model.Slug;
            articleToEdit.ImagePath = model.ImagePath;
            articleToEdit.ImageFileFormat = model.ImageFileFormat;
            articleToEdit.ImageFileName = model.ImageFileName;
            articleToEdit.ImageWidths = model.ImageWidths;
            articleToEdit.PublishBegin = model.PublishBegin;
            articleToEdit.PublishEnd = model.PublishEnd;
            articleToEdit.CompanyId = model.Company.CompanyId;
            articleToEdit.UserId = model.UserId;
            return articleToEdit;

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
                UserId = model.UserId

            };
        }
    }
}