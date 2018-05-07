using Domain.Entites;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        public IEnumerable<Article> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(Article article)
        {
            _repository.Update(article);
        }

        public string GenerateSlug(string tempSlug)
        {
            tempSlug = tempSlug.ToLower();
            tempSlug = Regex.Replace(tempSlug, "[äå]", "a");
            tempSlug = Regex.Replace(tempSlug, "[óòöôõø]", "o");
            tempSlug = Regex.Replace(tempSlug, "[úùüû]", "u");
            tempSlug = Regex.Replace(tempSlug, "[éèëê]", "e");
            tempSlug = Regex.Replace(tempSlug, @"\s", "-");
            tempSlug = Regex.Replace(tempSlug, @"\s+", " ").Trim();
            tempSlug = Regex.Replace(tempSlug, @"[^a-z0-9\s-]", "");
            return tempSlug;
        }      
    }
}
