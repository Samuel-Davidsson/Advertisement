using System;
using System.Collections.Generic;

namespace AnnonsonMVC.Models
{
    public partial class TUserStore
    {
        public int UserStoreId { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public bool IsAdministrator { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }

        public TStore Store { get; set; }
        public TUser User { get; set; }
    }
}
