using Domain.Entites;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Services
{
    public class StoreService
    {
        private readonly IRepository<Store> _repository;

        public StoreService(IRepository<Store> repository)
        {
            _repository = repository;
        }

        public void Add(Store store)
        {
            _repository.Add(store);
        }

        public Store Find(int id, params string[] includeProperties)
        {
            return _repository.Find(x => x.StoreId == id, includeProperties);
        }

        public IEnumerable<Store> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
