using System;
using System.Collections.Generic;

namespace AnnonsonMVC.Models
{
    public partial class TCompanyContract
    {
        public int CompanyContractId { get; set; }
        public int CompanyId { get; set; }
        public int ContractId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }

        public TCompany Company { get; set; }
        public TUser User { get; set; }
    }
}
