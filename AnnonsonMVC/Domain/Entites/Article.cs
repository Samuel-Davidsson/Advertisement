using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Article
    {
        public Article()
        {
            ArticleCategory = new HashSet<ArticleCategory>();
            StoreArticle = new HashSet<StoreArticle>();
        }

        public int ArticleId { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string ImageFileName { get; set; }
        public string ImageFileFormat { get; set; }
        public string ImageWidths { get; set; }
        public decimal Price { get; set; }
        public string PriceText { get; set; }
        public string PriceUnit { get; set; }
        public DateTime PublishBegin { get; set; }
        public DateTime PublishEnd { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }
        public string ImageUrl { get; set; }

        public Company Company { get; set; }
        public ICollection<ArticleCategory> ArticleCategory { get; set; }
        public ICollection<StoreArticle> StoreArticle { get; set; }
    }
}
