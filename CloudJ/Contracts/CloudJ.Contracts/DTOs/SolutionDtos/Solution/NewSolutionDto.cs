using CloudJ.Contracts.DTOs.SolutionDtos.Cloud;
using CloudJ.Contracts.DTOs.SolutionDtos.Photo;
using CloudJ.Contracts.DTOs.SolutionDtos.Plan;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.SolutionDtos.Solution
{
    public class NewSolutionDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public NewPhotoDto[] Photos { get; set; }
        public long CategoryId { get; set; }
        public NewCloudDto Cloud { get; set; }

    }
}
