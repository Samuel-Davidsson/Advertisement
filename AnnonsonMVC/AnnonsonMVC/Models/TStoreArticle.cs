using System;
using System.Collections.Generic;

namespace AnnonsonMVC.Models
{
    public partial class TStoreArticle
    {
        public int StoreId { get; set; }
        public int ArticleId { get; set; }

        public TArticle Article { get; set; }
        public TStore Store { get; set; }
    }
}
