using AnnonsonMVC.ViewModels;
using Domain.Entites;
using System.Collections.Generic;

namespace AnnonsonMVC.Utilities
{
    public class AddOrEditStoreArticle
    {
        public void CreateStoreArticles(Article article, List<int> selectedStoreListIds)
        {
            foreach (var storeId in selectedStoreListIds)
            {
                article.StoreArticle.Add(new StoreArticle
                {
                    StoreId = storeId,
                    ArticleId = article.ArticleId
                });
            }          
        }
        public void EditStoreArticles(ArticleEditViewModel model)
        {
            foreach (var storeId in model.StoreIds)
            {
                model.StoreArticle.Add(new StoreArticle
                {
                    StoreId = storeId,
                    ArticleId = model.ArticleId
                });
            }
        }
    }
}
