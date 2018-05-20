using Domain.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnnonsonMVC.ViewModels
{
    public class ArticleDetailViewModel
    {
        public int ArticleId { get; set; }
        [Display(Name = "Rubrik")]
        public string Name { get; set; }
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        [Display(Name = "Pris")]
        public decimal Price { get; set; }
        [Display(Name = "Pristext")]
        public string PriceText { get; set; }
        [Display(Name = "Prisenhet")]
        public string PriceUnit { get; set; }

        [Display(Name = "Startdatum")]
        [DataType(DataType.Date)]
        public DateTime PublishBegin { get; set; }
        [Display(Name = "Slutdatum")]
        [DataType(DataType.Date)]
        public DateTime PublishEnd { get; set; }

        [Display(Name = "Kategori")]
        public ICollection<ArticleCategory> ArticleCategory { get; set; } = new List<ArticleCategory>();
        [Display(Name = "Butiker")]
        public ICollection<StoreArticle> StoreArticle { get; set; } = new List<StoreArticle>();

        [Display(Name = "Företag")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string ImageFileName { get; set; }
        public string ImageFileFormat { get; set; }
        public string ImagePath { get; set; }
    }
}
