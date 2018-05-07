using Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICategoryService
    {
        void Add(Category category);
        IEnumerable<Category> GetAll();
        Category Find(int id, params string[] includeProperties);
    }
}