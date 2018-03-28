using System;
using System.Collections.Generic;

namespace AnnonsonMVC.Models
{
    public partial class CompanyContract
    {
        public int CompanyContractId { get; set; }
        public int CompanyId { get; set; }
        public int ContractId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }

        public Company Company { get; set; }
        public User User { get; set; }
    }
}
