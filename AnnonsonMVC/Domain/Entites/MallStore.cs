using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class MallStore
    {
        public int MallId { get; set; }
        public int StoreId { get; set; }

        public Mall Mall { get; set; }
        public Store Store { get; set; }
    }
}
