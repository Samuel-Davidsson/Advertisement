using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Seller
    {
        public Seller()
        {
            StoreSubscription = new HashSet<StoreSubscription>();
        }

        public int SellerId { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
        public string RegistrationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string AddressExtra { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool HasInvoice { get; set; }
        public bool HasCard { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }
        public string Subscriptions { get; set; }
        public bool? SendAdminMail { get; set; }
        public bool? SendCustomerReceipt { get; set; }
        public bool? SendSellerConfirmation { get; set; }
        public string SellerConfirmationEmail { get; set; }

        public ICollection<StoreSubscription> StoreSubscription { get; set; }
    }
}
