using Domain.Entites;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Services
{
    public class ArticleService
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

        public IEnumerable<Article> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
