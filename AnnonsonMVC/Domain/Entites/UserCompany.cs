using System;

namespace Domain.Entites
{
    public partial class UserCompany
    {
        public int UserCompanyId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public bool IsAdministrator { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }

        public Company Company { get; set; }
        public User User { get; set; }
    }
}
