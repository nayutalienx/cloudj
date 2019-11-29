using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.OrderDtos
{
    public class BalanceDto
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public double BalanceValue { get; set; }
    }
}
