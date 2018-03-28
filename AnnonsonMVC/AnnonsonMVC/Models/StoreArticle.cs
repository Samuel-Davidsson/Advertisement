using System;
using System.Collections.Generic;

namespace AnnonsonMVC.Models
{
    public partial class StoreArticle
    {
        public int StoreId { get; set; }
        public int ArticleId { get; set; }

        public Article Article { get; set; }
        public Store Store { get; set; }
    }
}
