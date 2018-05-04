using Domain.Entites;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class StoreArticleService: IStoreArticleService
    {
        private readonly IRepository<StoreArticle> _repository;

        public StoreArticleService(IRepository<StoreArticle> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<StoreArticle>> GetAll()
        {
            return await _repository.GetAll();
        }
        public void Update(StoreArticle storeArticle)
        {
            _repository.Update(storeArticle);
        }
    }
}
