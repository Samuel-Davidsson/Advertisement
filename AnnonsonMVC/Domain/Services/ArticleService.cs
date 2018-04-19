using Domain.Entites;
using Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository<Article> _repository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ArticleService(IRepository<Article> repository, IHostingEnvironment hostingEnvironment)
        {
            _repository = repository;
            _hostingEnvironment = hostingEnvironment;
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

        public string CreateImageDirectory(Article newArticle)
        {
            newArticle.ImageFileName = "aid" + newArticle.ArticleId + "-" + Guid.NewGuid();
            string year = DateTime.Now.ToString("yyyy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");

            var todaysDate = year + @"\" + month + @"\" + day;
            var uploadpath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads\\" + todaysDate);

            if (!Directory.Exists(uploadpath))
            {
                Directory.CreateDirectory(uploadpath);
            }

            return uploadpath;
        }
    }
}
