﻿using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class ArticleCategory
    {
        public int ArticleId { get; set; }
        public int CategoryId { get; set; }

        public Article Article { get; set; }
        public Category Category { get; set; }
    }
}