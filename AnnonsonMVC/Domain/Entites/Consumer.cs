using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Consumer
    {
        public int Id { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public bool? ValidMobile { get; set; }
        public bool? ValidEmail { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public string Code { get; set; }
    }
}
