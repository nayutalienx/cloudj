using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Solution.Models
{
    public class SolutionLink : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public long SolutionId { get; set; }
        public virtual Solution Solution { get; set; }

    }
}
