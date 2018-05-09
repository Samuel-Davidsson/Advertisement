using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Country
    {
        public Country()
        {
            Contract = new HashSet<Contract>();
            Region = new HashSet<Region>();
            Subscription = new HashSet<Subscription>();
            User = new HashSet<User>();
        }

        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }

        public ICollection<Contract> Contract { get; set; }
        public ICollection<Region> Region { get; set; }
        public ICollection<Subscription> Subscription { get; set; }
        public ICollection<User> User { get; set; }
    }
}
