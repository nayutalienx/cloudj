using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models.Solution
{
    public class Review : BaseEntity
    {
        public string Header { get; set; }
        public string Text { get; set; }
        public DateTime PostedTime { get; set; }
        public byte Rate { get; set; }
        public string AuthorId { get; set; }
        public long SolutionId { get; set; }
        public virtual Solution Solution { get; set; }
    }
}
