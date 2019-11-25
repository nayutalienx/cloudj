using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.SolutionDtos
{
    public class SolutionDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Rate { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public PhotoDto Logo { get; set; }
        public PhotoDto[] Photos { get; set; }
        public PlanDto[] Plans { get; set; }
        public ReviewDto[] Reviews { get; set; }
        public CategoryDto Category { get; set; }
        public SoluitonLink[] InfoLinks { get; set; }

    }
}
