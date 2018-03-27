using System;
using System.Collections.Generic;

namespace AnnonsonMVC.Models
{
    public partial class TArticleCategory
    {
        public int ArticleId { get; set; }
        public int CategoryId { get; set; }

        public TArticle Article { get; set; }
        public TCategory Category { get; set; }
    }
}
