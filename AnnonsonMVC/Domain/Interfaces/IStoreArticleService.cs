using Domain.Entites;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IStoreArticleService
    {
        IEnumerable<StoreArticle> GetAll();
        void Update(StoreArticle storeArticle);
    }
}
