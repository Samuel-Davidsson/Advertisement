using Domain.Entites;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository<Article> _repository;

        public ArticleService(IRepository<Article> repository)
        {
            _repository = repository;
        }

        public void Add(Article article)
        {
            _repository.Add(article);
        }

        public Article Find(int id, params string[] includeProperties)
        {
            return _repository.Find(x => x.ArticleId == id, includeProperties);
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await _repository.GetAll();
        }

        public void Update(Article article)
        {
            _repository.Update(article);
        }
    }
}
