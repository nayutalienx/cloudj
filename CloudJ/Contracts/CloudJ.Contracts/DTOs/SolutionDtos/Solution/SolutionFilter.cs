using CloudJ.Contracts.DTOs.SolutionDtos.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.SolutionDtos.Solution
{
    public class SolutionFilter
    {
        public int Size { get; set; } = 5;
        public int Page { get; set; } = 1;
        public long? SolutionId { get; set; }
        public long? DeveloperId { get; set; }
        public long? CategoryId { get; set; }

    }
}
