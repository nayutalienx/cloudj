
using CloudJ.Contracts.DTOs.SolutionDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.OrderDtos
{
    public class OrderDto
    {
        public UserDto Customer { get; set; }
        public SolutionDto Solution { get; set; }
        public PlanDto Plan { get; set; }
    }
}
