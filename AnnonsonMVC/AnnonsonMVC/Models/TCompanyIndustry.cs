using System;
using System.Collections.Generic;

namespace AnnonsonMVC.Models
{
    public partial class TCompanyIndustry
    {
        public int CompanyId { get; set; }
        public int IndustryId { get; set; }

        public TCompany Company { get; set; }
        public TIndustry Industry { get; set; }
    }
}
