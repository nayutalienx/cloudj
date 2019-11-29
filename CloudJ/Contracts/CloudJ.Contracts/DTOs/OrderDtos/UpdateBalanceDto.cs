using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.OrderDtos
{
    public class UpdateBalanceDto
    {
        public string UserId { get; set; }
        public double BalanceValue { get; set; }
    }
}
