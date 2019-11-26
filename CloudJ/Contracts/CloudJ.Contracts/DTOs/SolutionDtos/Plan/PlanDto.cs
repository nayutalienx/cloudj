using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.SolutionDtos.Plan
{
    public class PlanDto
    {
        public long Id { get; set; }
        public long SolutionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; } 
        public string Time { get; set; } 
    }
}
