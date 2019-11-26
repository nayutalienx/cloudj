using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.SolutionDtos.Solution
{
    public class NewSolutionLinkDto
    {
        public long SolutionId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
