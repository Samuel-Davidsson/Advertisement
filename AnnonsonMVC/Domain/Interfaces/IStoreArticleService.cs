using Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IStoreArticleService
    {
        Task<IEnumerable<StoreArticle>> GetAll();
    }
}
