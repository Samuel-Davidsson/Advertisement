using System;
using System.Collections.Generic;

namespace AnnonsonMVC.Models
{
    public partial class TUser
    {
        public TUser()
        {
            TCompanyContract = new HashSet<TCompanyContract>();
            TUserCompany = new HashSet<TUserCompany>();
            TUserStore = new HashSet<TUserStore>();
        }

        public int UserId { get; set; }
        public int CountryId { get; set; }
        public int LanguageId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }

        public ICollection<TCompanyContract> TCompanyContract { get; set; }
        public ICollection<TUserCompany> TUserCompany { get; set; }
        public ICollection<TUserStore> TUserStore { get; set; }
    }
}
