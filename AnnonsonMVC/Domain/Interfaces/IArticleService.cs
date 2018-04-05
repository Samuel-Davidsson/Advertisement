using Domain.Entites;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IArticleService
    {
        void Add(Article article);
        //void Update(Article article);
        IEnumerable<Article> GetAll();
    }
}