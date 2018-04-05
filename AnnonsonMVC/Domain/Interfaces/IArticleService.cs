using Domain.Entites;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IArticleService
    {
        void Add(Article article);
        IEnumerable<Article> GetAll();
    }
}