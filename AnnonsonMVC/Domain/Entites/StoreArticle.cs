namespace Domain.Entites
{
    public partial class StoreArticle
    {
        public int StoreId { get; set; }
        public int ArticleId { get; set; }
        public int StoreArticleId { get; set; }

        public Article Article { get; set; }
        public Store Store { get; set; }
    }
}
