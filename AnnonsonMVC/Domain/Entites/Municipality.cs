using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Municipality
    {
        public Municipality()
        {
            Store = new HashSet<Store>();
        }

        public int MunicipalityId { get; set; }
        public int RegionId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }

        public Region Region { get; set; }
        public ICollection<Store> Store { get; set; }
    }
}
