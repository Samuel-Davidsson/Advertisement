using Domain.Entites;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Services
{
    public class CategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public void Add(Category category)
        {
            _repository.Add(category);
        }

        public Category Find(int id, params string[] includeProperties)
        {
            return _repository.Find(x => x.CategoryId == id, includeProperties);
        }

        public IEnumerable<Category> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
