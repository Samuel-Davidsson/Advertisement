using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Mall
    {
        public Mall()
        {
            MallStore = new HashSet<MallStore>();
        }

        public int MallId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }

        public ICollection<MallStore> MallStore { get; set; }
    }
}
