using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Solution.Models
{
    public class Photo : BaseEntity
    {
        public byte[] Data { get; set; }
        public string Type { get; set; }
        public long SolutionId { get; set; }
        public virtual Solution Solution { get; set; }
    }
}
