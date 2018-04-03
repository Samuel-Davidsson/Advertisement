using AnnonsonMVC.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace AnnonsonMVC.ViewModels
{
    public class ArticelViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PriceText { get; set; }
        public string PriceUnit { get; set; }

        public DateTime PublishBegin { get; set; }
        public DateTime PublishEnd { get; set; }

        public IFormFile Image { get; set; }

        public Company Company { get; set; }

        public ICollection<ArticleCategory> ArticleCategory { get; set; }
        public ICollection<StoreArticle> StoreArticle { get; set; }
    }
}
