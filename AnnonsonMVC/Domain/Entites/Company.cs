using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Company
    {
        public Company()
        {
            Article = new HashSet<Article>();
            CompanyContract = new HashSet<CompanyContract>();
            CompanyIndustry = new HashSet<CompanyIndustry>();
            Store = new HashSet<Store>();
            UserCompany = new HashSet<UserCompany>();
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

        public ICollection<Article> Article { get; set; }
        public ICollection<CompanyContract> CompanyContract { get; set; }
        public ICollection<CompanyIndustry> CompanyIndustry { get; set; }
        public ICollection<Store> Store { get; set; }
        public ICollection<UserCompany> UserCompany { get; set; }
    }
}
