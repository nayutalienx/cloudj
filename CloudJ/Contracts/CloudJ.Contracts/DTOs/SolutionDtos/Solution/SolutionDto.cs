using CloudJ.Contracts.DTOs.SolutionDtos.Category;
using CloudJ.Contracts.DTOs.SolutionDtos.Cloud;
using CloudJ.Contracts.DTOs.SolutionDtos.Photo;
using CloudJ.Contracts.DTOs.SolutionDtos.Plan;
using CloudJ.Contracts.DTOs.SolutionDtos.Review;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.SolutionDtos.Solution
{
    public class SolutionDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Rate { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public PhotoDto[] Photos { get; set; }
        public PlanDto[] Plans { get; set; }
        public ReviewDto[] Reviews { get; set; }
        public CategoryDto Category { get; set; }
        public SolutionLinkDto[] InfoLinks { get; set; }
        public CloudDto Cloud { get; set; }

    }
}
