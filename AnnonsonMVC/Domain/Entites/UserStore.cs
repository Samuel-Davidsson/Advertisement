using System;

namespace Domain.Entites
{
    public partial class UserStore
    {
        public int UserStoreId { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public bool IsAdministrator { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }

        public Store Store { get; set; }
        public User User { get; set; }
    }
}
