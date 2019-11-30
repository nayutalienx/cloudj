
using CloudJ.Contracts.DTOs.SolutionDtos;
using CloudJ.Contracts.DTOs.SolutionDtos.Plan;
using CloudJ.Contracts.DTOs.SolutionDtos.Solution;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.OrderDtos
{
    public class OrderDto
    {
        public string CustomerId { get; set; }
        public SolutionDto Solution { get; set; }
        public DateTime CreatedTime { get; set; }
        public PlanDto Plan { get; set; }
    }
}
