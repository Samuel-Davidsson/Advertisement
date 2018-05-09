using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Discount
    {
        public int DiscountId { get; set; }
        public int SubscriptionId { get; set; }
        public int SellerId { get; set; }
        public decimal Amount { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }

        public Subscription Subscription { get; set; }
    }
}
