using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class SubscriptionPaymentMethod
    {
        public int SubscriptionId { get; set; }
        public int PaymentMethodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public Subscription Subscription { get; set; }
    }
}
