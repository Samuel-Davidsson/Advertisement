using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class NotificationOutcome
    {
        public int NotificationId { get; set; }
        public int NotificationPlatformId { get; set; }
        public bool Enqueued { get; set; }
        public string TrackingId { get; set; }
        public string Payload { get; set; }

        public Notification Notification { get; set; }
        public NotificationPlatform NotificationPlatform { get; set; }
    }
}
