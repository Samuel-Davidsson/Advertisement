using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class ArticleLog
    {
        public int ArticleLogId { get; set; }
        public int ArticleId { get; set; }
        public int StoreId { get; set; }
        public DateTime Date { get; set; }
        public string IpAddress { get; set; }
        public bool FromApp { get; set; }
        public string OperatingSystem { get; set; }
    }
}
