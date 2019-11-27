using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.OrderDtos
{
    public class NewOrderDto
    {
        public string CustomerId { get; set; }
        public long SolutionId { get; set; }
        public long PlanId { get; set; }
    }
}
