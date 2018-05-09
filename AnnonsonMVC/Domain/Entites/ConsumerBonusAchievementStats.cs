using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class ConsumerBonusAchievementStats
    {
        public int Id { get; set; }
        public int ConsumerBonusId { get; set; }
        public int BonusId { get; set; }
        public int StoreId { get; set; }
        public DateTime Date { get; set; }
    }
}
