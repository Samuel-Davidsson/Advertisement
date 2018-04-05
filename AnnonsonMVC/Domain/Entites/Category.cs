using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Category
    {
        public Category()
        {
            InverseParent = new HashSet<Category>();
            ArticleCategory = new HashSet<ArticleCategory>();
        }

        public int CategoryId { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public int Depth { get; set; }
        public string Path { get; set; }
        public string PathSlug { get; set; }
        public int HierarcyIndex { get; set; }
        public int Childs { get; set; }
        public int ChildsPublished { get; set; }
        public int ChildsClosestPublished { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }

        public Category Parent { get; set; }
        public ICollection<Category> InverseParent { get; set; }
        public ICollection<ArticleCategory> ArticleCategory { get; set; }
    }
}
