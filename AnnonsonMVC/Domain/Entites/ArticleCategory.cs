namespace Domain.Entites
{
    public partial class ArticleCategory
    {
        public int ArticleCategoryId { get; set; }
        public int ArticleId { get; set; }
        public int CategoryId { get; set; }

        public Article Article { get; set; }
        public Category Category { get; set; }
    }
}
