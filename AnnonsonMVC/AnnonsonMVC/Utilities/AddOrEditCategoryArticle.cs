using AnnonsonMVC.ViewModels;
using Domain.Entites;

namespace AnnonsonMVC.Utilities
{
    public class AddOrEditCategoryArticle
    {
        public void CreateCategoryArticle(Article article, int categoryId)
        {
            article.ArticleCategory.Add(new ArticleCategory
            {
                ArticleId = article.ArticleId,
                CategoryId = categoryId,
            });
        }

        public void EditCategoryArticle(ArticleEditViewModel model)
        {
            model.ArticleCategory.Add(new ArticleCategory
            {
                ArticleId = model.ArticleId,
                CategoryId = model.CategoryId
            });
        }

    }
}
