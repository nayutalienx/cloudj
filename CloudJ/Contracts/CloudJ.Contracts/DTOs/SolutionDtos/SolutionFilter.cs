using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.SolutionDtos
{
    public class SolutionFilter
    {
        public int Size { get; set; }
        public long? SolutionId { get; set; }
        public long? DeveloperId { get; set; }
        public CategoryDto Category { get; set; }

    }
}
