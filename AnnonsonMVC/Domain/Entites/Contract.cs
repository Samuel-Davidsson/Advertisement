using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Contract
    {
        public Contract()
        {
            CompanyContract = new HashSet<CompanyContract>();
        }

        public int ContractId { get; set; }
        public int CountryId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }

        public Country Country { get; set; }
        public ICollection<CompanyContract> CompanyContract { get; set; }
    }
}
