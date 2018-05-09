using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Subscription
    {
        public Subscription()
        {
            Discount = new HashSet<Discount>();
            StoreSubscription = new HashSet<StoreSubscription>();
            SubscriptionPaymentMethod = new HashSet<SubscriptionPaymentMethod>();
        }

        public int SubscriptionId { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public int Length { get; set; }
        public int ArticleLimit { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }

        public Country Country { get; set; }
        public ICollection<Discount> Discount { get; set; }
        public ICollection<StoreSubscription> StoreSubscription { get; set; }
        public ICollection<SubscriptionPaymentMethod> SubscriptionPaymentMethod { get; set; }
    }
}
