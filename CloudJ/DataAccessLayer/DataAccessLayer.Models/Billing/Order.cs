using DataAccessLayer.Models;
using DataAccessLayer.Models.Solution;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models.Billing
{
    public class Order : BaseEntity
    {
        public string CustomerId { get; set; }
        public long SolutionId { get; set; }
        public virtual Solution.Solution Solution { get; set; }
        public long PlanId { get; set; }
        public virtual Plan Plan { get; set; }
    }
}
