using Domain.Entites;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Services
{
    public class StoreArticleService: IStoreArticleService
    {
        private readonly IRepository<StoreArticle> _repository;

        public StoreArticleService(IRepository<StoreArticle> repository)
        {
            _repository = repository;
        }
        public IEnumerable<StoreArticle> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
