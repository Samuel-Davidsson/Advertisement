using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class ConsumerBonus
    {
        public int Id { get; set; }
        public int ConsumerId { get; set; }
        public int BonusId { get; set; }
        public int? Stamp { get; set; }
        public int? Points { get; set; }
        public DateTime Date { get; set; }
        public bool IsAchieved { get; set; }
        public int PayedOut { get; set; }
    }
}
