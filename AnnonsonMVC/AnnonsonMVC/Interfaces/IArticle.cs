using AnnonsonMVC.Models;
using System;
using System.Collections.Generic;

namespace AnnonsonMVC.Interfaces
{
    public interface IArticle
    {

        int ArticleId { get; set; }
        int CompanyId { get; set; }
        int UserId { get; set; }
        string Name { get; set; }
        string Slug { get; set; }
        string Description { get; set; }
        string ImagePath { get; set; }
        string ImageFileName { get; set; }
        string ImageFileFormat { get; set; }
        string ImageWidths { get; set; }
        decimal Price { get; set; }
        string PriceText { get; set; }
        string PriceUnit { get; set; }
        DateTime PublishBegin { get; set; }
        DateTime PublishEnd { get; set; }
        bool IsDeleted { get; set; }
        DateTime Modified { get; set; }
        DateTime Created { get; set; }
        DateTime Deleted { get; set; }
        string ImageUrl { get; set; }

        Company Company { get; set; }
        ICollection<ArticleCategory> ArticleCategory { get; set; }
        ICollection<StoreArticle> StoreArticle { get; set; }
    }
}