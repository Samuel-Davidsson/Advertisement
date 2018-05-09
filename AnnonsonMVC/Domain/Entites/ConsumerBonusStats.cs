using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class ConsumerBonusStats
    {
        public int Id { get; set; }
        public int ConsumerId { get; set; }
        public int BonusId { get; set; }
        public int StoreId { get; set; }
        public int? Value { get; set; }
        public DateTime Date { get; set; }
    }
}
