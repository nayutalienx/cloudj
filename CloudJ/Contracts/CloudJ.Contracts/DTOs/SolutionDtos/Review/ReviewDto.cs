using CloudJ.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.SolutionDtos.Review
{
    public class ReviewDto
    {
        public string Header { get; set; }
        public string Text { get; set; }
        public DateTime PostedTime { get; set; }
        public byte Rate { get; set; }
        public UserDto Author { get; set; }
    }
}
