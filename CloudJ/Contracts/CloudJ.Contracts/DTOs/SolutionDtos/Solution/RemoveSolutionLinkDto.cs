using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.SolutionDtos.Solution
{
    public class RemoveSolutionLinkDto
    {
        public long Id { get; set; }
        public long? SolutionId { get; set; }
    }
}
