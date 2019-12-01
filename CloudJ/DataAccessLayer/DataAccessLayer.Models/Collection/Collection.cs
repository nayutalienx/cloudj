using DataAccessLayer.Models.Solution;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models.Collection
{
    public class Collection : BaseEntity
    {
       
        public string Name { get; set; }
        public string Preview { get; set; }
        public string Description { get; set; }
        //public virtual ICollection<Solution.Solution> Solutions { get; set; }
        public virtual ICollection<TruncSolution> Solutions { get; set; }
        public virtual Photo PhotoPreview { get; set; }
    }
}
