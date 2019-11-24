using CloudJ.Contracts.SolutionDtos.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.OrderDtos
{
    public class OrderFilter
    {
        public UserDto Customer { get; set; }
        public SolutionDto Solution { get; set; }
    }
}
