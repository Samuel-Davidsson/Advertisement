using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class NotificationPlatform
    {
        public NotificationPlatform()
        {
            NotificationOutcome = new HashSet<NotificationOutcome>();
        }

        public int NotificationPlatformId { get; set; }
        public string Name { get; set; }

        public ICollection<NotificationOutcome> NotificationOutcome { get; set; }
    }
}
