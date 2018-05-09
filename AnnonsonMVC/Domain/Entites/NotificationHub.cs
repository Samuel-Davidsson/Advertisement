using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class NotificationHub
    {
        public NotificationHub()
        {
            Notification = new HashSet<Notification>();
        }

        public int NotificationHubId { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string SendAccessSignature { get; set; }

        public Store Store { get; set; }
        public ICollection<Notification> Notification { get; set; }
    }
}
