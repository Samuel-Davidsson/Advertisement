using Domain.Entites;
using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Language
    {
        public Language()
        {
            User = new HashSet<User>();
        }

        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsPublished { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }

        public ICollection<User> User { get; set; }
    }
}
