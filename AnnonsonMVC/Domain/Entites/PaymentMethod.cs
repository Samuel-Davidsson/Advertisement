using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            SubscriptionPaymentMethod = new HashSet<SubscriptionPaymentMethod>();
        }

        public int PaymentMethodId { get; set; }
        public int PaymentOperatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }

        public ICollection<SubscriptionPaymentMethod> SubscriptionPaymentMethod { get; set; }
    }
}
