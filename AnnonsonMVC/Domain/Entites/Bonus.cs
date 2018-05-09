using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Bonus
    {
        public Bonus()
        {
            BonusStore = new HashSet<BonusStore>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public int MinPurchase { get; set; }
        public int Points { get; set; }
        public int Money { get; set; }
        public int GoalPoints { get; set; }
        public string Product { get; set; }
        public int NumberOfProducts { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? Deleted { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<BonusStore> BonusStore { get; set; }
    }
}
