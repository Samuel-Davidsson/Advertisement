using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class CompanyIndustry
    {
        public int CompanyId { get; set; }
        public int IndustryId { get; set; }

        public Company Company { get; set; }
        public Industry Industry { get; set; }
    }
}
