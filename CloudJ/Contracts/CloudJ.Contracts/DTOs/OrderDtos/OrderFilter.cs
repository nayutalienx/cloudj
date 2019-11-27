
using CloudJ.Contracts.DTOs.SolutionDtos;
using CloudJ.Contracts.DTOs.SolutionDtos.Solution;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.OrderDtos
{
    public class OrderFilter
    {
        public string CustomerId { get; set; }
        public long? SolutionId { get; set; }
        public long? OrderId { get; set; }
    }
}
