
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models.Solution
{
    public class Solution : BaseEntity
    {
        
        public string Name { get; set; } 
        public string Description { get; set; }  
        public byte Rate { get; set; } 
        public string UserId { get; set; }
        public DateTime CreatedTime { get; set; } 
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<SolutionLink> SolutionLinks { get; set; }
        public virtual Cloud Cloud { get; set; }
        
    }
}
