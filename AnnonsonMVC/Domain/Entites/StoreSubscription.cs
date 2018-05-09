using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class StoreSubscription
    {
        public int StoreSubscriptionId { get; set; }
        public int StoreId { get; set; }
        public int SubscriptionId { get; set; }
        public int SellerId { get; set; }
        public int CompanyId { get; set; }
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public DateTime Date { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Length { get; set; }
        public int ArticleLimit { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Vat { get; set; }
        public bool IsPaid { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }
        public bool IsAutoRenew { get; set; }
        public string StripeId { get; set; }

        public Seller Seller { get; set; }
        public Store Store { get; set; }
        public Subscription Subscription { get; set; }
    }
}
