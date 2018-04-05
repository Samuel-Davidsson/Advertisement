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
            return articleToEdit;

    }
    }
}