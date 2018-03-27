using System;
using System.Collections.Generic;

namespace AnnonsonMVC.Models
{
    public partial class TUserCompany
    {
        public int UserCompanyId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public bool IsAdministrator { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }

        public TCompany Company { get; set; }
        public TUser User { get; set; }
    }
}
