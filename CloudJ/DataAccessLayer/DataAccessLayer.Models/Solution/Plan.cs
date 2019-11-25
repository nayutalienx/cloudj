using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models.Solution
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Time { get; set; }
        public long SolutionId { get; set; }
        public virtual Solution Solution { get; set; }
    }
}
