using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Region
    {
        public Region()
        {
            Municipality = new HashSet<Municipality>();
        }

        public int RegionId { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }

        public Country Country { get; set; }
        public ICollection<Municipality> Municipality { get; set; }
    }
}
