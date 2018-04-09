using Domain.Entites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace AnnonsonMVC.ViewModels
{
    public class ArticelViewModel
    {
        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PriceText { get; set; }
        public string PriceUnit { get; set; }
        public string Slug { get; set; }
        public string ImagePath { get; set; }
        public string ImageFileName { get; set; }
        public string ImageFileFormat { get; set; }
        public string ImageWidths { get; set; }

        public DateTime PublishBegin { get; set; }
        public DateTime PublishEnd { get; set; }

        public IFormFile File { get; set; }

        public Company Company { get; set; }
        public Store Store { get; set; }
        public Category Category { get; set; }

        public ICollection<ArticleCategory> ArticleCategory { get; set; }
        public ICollection<StoreArticle> StoreArticle { get; set; }
    }
}
