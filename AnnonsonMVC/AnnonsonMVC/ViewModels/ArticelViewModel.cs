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
        }

        public int ArticleId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Artklen måste ha en rubrik.")]
        [DataType(DataType.Text)]
        [StringLength(250, MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Artklen måste ha en beskrivning.")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required(ErrorMessage ="Artklen måste ha ett pris.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Artklen måste ha en pristext.")]
        [StringLength(50, MinimumLength = 1)]
        public string PriceText { get; set; }

        [Required(ErrorMessage = "Artklen måste ha en prisenhet.")]
        [StringLength(50, MinimumLength = 1)]
        public string PriceUnit { get; set; }

        public string Slug { get; set; }
        public string ImagePath { get; set; }
        public string ImageFileName { get; set; }
        public string ImageFileFormat { get; set; }
        public string ImageWidths { get; set; }

        [Required(ErrorMessage ="Artikeln måste ha ett startdatum.")]
        [DataType(DataType.Date)]
        public DateTime PublishBegin { get; set; }

        [Required(ErrorMessage = "Artikeln måste ha ett slutdatum.")]
        [DataType(DataType.Date)]
        public DateTime PublishEnd { get; set; }

        [Required(ErrorMessage = "Artikeln måste ha en bild.")]
        public IFormFile ImageFile { get; set; }

        public Company Company { get; set; }

        public List<SelectListItem> Stores { get; set; }
        public int[] StoreIds { get; set; }


        public ICollection<ArticleCategory> ArticleCategory { get; set; }
        public ICollection<StoreArticle> StoreArticle { get; set; }
    }
}
