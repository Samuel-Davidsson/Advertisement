using Domain.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnnonsonMVC.ViewModels
{
    public class ArticelViewModel
    {
        public ArticelViewModel()
        {
            ArticleCategory = new HashSet<ArticleCategory>();
            StoreArticle = new HashSet<StoreArticle>();
            Category = new Category();
    }
        public int ArticleId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string PriceText { get; set; }
        [Required]
        public string PriceUnit { get; set; }

        public string Slug { get; set; }
        public string ImagePath { get; set; }
        public string ImageFileName { get; set; }
        public string ImageFileFormat { get; set; }
        public string ImageWidths { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishBegin { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishEnd { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }

        public Company Company { get; set; }
        public Category Category { get; set; }

        public List<SelectListItem> Stores { get; set; }
        public int[] StoreIds { get; set; }


        public ICollection<ArticleCategory> ArticleCategory { get; set; }
        public ICollection<StoreArticle> StoreArticle { get; set; }
    }
}
