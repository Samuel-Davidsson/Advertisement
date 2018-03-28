using System;

namespace AnnonsonMVC.ViewModels
{
    public class ArticelViewModel
    {
        public int ArticleId { get; set; }
        public string Name { get; set; }
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
    }
}
