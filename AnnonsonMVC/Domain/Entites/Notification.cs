using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Notification
    {
        public Notification()
        {
            NotificationOutcome = new HashSet<NotificationOutcome>();
        }

        public int NotificationId { get; set; }
        public int NotificationHubId { get; set; }
        public int UserId { get; set; }
        public int? ArticleId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }

        public Article Article { get; set; }
        public NotificationHub NotificationHub { get; set; }
        public User User { get; set; }
        public ICollection<NotificationOutcome> NotificationOutcome { get; set; }
    }
}
