using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class ApplicationLog
    {
        public int ApplicationLogId { get; set; }
        public DateTime Date { get; set; }
        public string Application { get; set; }
        public string Type { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
    }
}
