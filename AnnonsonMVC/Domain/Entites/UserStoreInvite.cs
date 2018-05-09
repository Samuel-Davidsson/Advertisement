using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class UserStoreInvite
    {
        public int UserStoreInviteId { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public string GuId { get; set; }
        public DateTime Date { get; set; }
        public string RecipientEmail { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsCompleted { get; set; }

        public Store Store { get; set; }
        public User User { get; set; }
    }
}
