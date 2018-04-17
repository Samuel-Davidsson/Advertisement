using Domain.Entites;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CategoryService : ICategoryService
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

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}
