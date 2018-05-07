using Domain.Entites;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IArticleCategoryService
    {
        IEnumerable<ArticleCategory> GetAll();
        void Update(ArticleCategory storeArticle);
    }
}