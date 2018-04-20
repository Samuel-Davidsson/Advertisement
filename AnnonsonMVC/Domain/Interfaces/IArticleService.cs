using Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IArticleService
    {
        void Add(Article article);
        void Update(Article article);
        Task<IEnumerable<Article>> GetAll();
        Article Find(int id, params string[] includeProperties);
        string GenerateSlug(string tempSlug);
    }
}