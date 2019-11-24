using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Billing.Models
{
    public class Order : BaseEntity
    {
        public string CustomerId { get; set; }
        public long SolutionId { get; set; }
        public virtual Solution.Models.Solution Solution { get; set; }
        public long PlanId { get; set; }
        public virtual Solution.Models.Plan Plan { get; set; }
    }
}
