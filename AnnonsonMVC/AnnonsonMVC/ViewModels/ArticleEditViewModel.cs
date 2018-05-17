using AnnonsonMVC.Validations;
using Domain.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace AnnonsonMVC.ViewModels
{
    public class ArticleEditViewModel
    {
        public ArticleEditViewModel()
        {
            ArticleCategory = new Collection<ArticleCategory>();
            StoreArticle = new Collection<StoreArticle>();
        }

        public int ArticleId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Artikeln måste ha en rubrik.")]
        [Display(Name = "Rubrik")]
        [DataType(DataType.Text)]
        [StringLength(250, MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Artikeln måste ha en beskrivning.")]
        [Display(Name = "Beskrivning")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Artikeln måste ha ett pris.")]
        [Display(Name = "Pris")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Artikeln måste ha en pristext.")]
        [Display(Name = "Pristext")]
        [StringLength(50, MinimumLength = 1)]
        public string PriceText { get; set; }

        [Required(ErrorMessage = "Artikeln måste ha en prisenhet.")]
        [Display(Name = "Prisenhet")]
        [StringLength(50, MinimumLength = 1)]
        public string PriceUnit { get; set; }

        public string Slug { get; set; }
        public string ImagePath { get; set; }
        public string ImageFileName { get; set; }
        public string ImageFileFormat { get; set; }
        public string ImageWidths { get; set; }

        [Required(ErrorMessage = "Artikeln måste ha ett startdatum.")]
        [Display(Name = "Startdatum")]
        [DataType(DataType.Date)]
        public DateTime PublishBegin { get; set; }

        [Required(ErrorMessage = "Artikeln måste ha ett slutdatum.")]
        [Display(Name = "Slutdatum")]
        [DataType(DataType.Date)]
        public DateTime PublishEnd { get; set; }

        public IFormFile ImageFile { get; set; }

        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public List<SelectListItem> Stores { get; set; }
        [Required(ErrorMessage = "Artikeln måste tillhöra en eller flera butiker.")]
        public int[] StoreIds { get; set; }

        [Display(Name = "Kategori")]
        public ICollection<ArticleCategory> ArticleCategory { get; set; } = new List<ArticleCategory>();
        [Display(Name = "Butiker")]
        public ICollection<StoreArticle> StoreArticle { get; set; } = new List<StoreArticle>();
    }
    }

