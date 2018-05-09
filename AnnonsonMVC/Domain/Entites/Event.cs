using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Event
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TicketUrl { get; set; }
        public string WebUrl { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int RegionId { get; set; }
        public string Organizer { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
