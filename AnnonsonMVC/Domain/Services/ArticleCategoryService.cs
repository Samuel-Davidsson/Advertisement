using Domain.Entites;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Services
{
    public class ArticleCategoryService : IArticleCategoryService
    {
        private readonly IRepository<ArticleCategory> _repository;

        public ArticleCategoryService(IRepository<ArticleCategory> repository)
        {
            _repository = repository;
        }
        public IEnumerable<ArticleCategory> GetAll()
        {
            return _repository.GetAll();
        }
        public void Update(ArticleCategory articleCategory)
        {
            _repository.Update(articleCategory);
        }
    }
}

