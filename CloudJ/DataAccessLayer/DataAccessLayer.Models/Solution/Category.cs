using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models.Solution
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public long? LogoId { get; set; }
        public virtual Photo Logo { get; set; }
        public long? ParentCategoryId { get; set; }
        public string Description { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Solution> Solutions { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }
    }
}
