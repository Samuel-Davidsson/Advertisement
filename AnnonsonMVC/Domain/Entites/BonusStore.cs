using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class BonusStore
    {
        public int BonusId { get; set; }
        public int StoreId { get; set; }

        public Bonus Bonus { get; set; }
        public Store Store { get; set; }
    }
}
