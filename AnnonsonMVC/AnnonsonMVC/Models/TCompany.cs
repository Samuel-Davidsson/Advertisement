using System;
using System.Collections.Generic;

namespace AnnonsonMVC.Models
{
    public partial class TCompany
    {
        public TCompany()
        {
            TArticle = new HashSet<TArticle>();
            TCompanyContract = new HashSet<TCompanyContract>();
            TCompanyIndustry = new HashSet<TCompanyIndustry>();
            TStore = new HashSet<TStore>();
            TUserCompany = new HashSet<TUserCompany>();
        }

        public int CompanyId { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string RegistrationNumber { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string AddressExtra { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string InvoiceAddress { get; set; }
        public string InvoiceAddressExtra { get; set; }
        public string InvoiceZipCode { get; set; }
        public string InvoiceCity { get; set; }
        public string InvoiceCountry { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }
        public bool IsDemo { get; set; }
        public string StripeId { get; set; }

        public ICollection<TArticle> TArticle { get; set; }
        public ICollection<TCompanyContract> TCompanyContract { get; set; }
        public ICollection<TCompanyIndustry> TCompanyIndustry { get; set; }
        public ICollection<TStore> TStore { get; set; }
        public ICollection<TUserCompany> TUserCompany { get; set; }
    }
}
