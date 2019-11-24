using DataAccessLayer.Models;
using System;

namespace DataAccessLayer.Solution.Models
{
    public class Solution : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Rate { get; set; }
        public DateTime CreatedTime { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
