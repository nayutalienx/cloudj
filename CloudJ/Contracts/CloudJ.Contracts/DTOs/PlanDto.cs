using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs
{
    class PlanDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Time { get; set; }
    }
}
