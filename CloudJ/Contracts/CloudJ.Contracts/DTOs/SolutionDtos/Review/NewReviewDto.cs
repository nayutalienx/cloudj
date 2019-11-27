using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.SolutionDtos.Review
{
    public class NewReviewDto
    {
        public long SolutionId { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public byte Rate { get; set; }
        public string AuthorId { get; set; }
    }
}
