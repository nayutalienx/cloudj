using CloudJ.Contracts.DTOs.SolutionDtos.Category;
using CloudJ.Contracts.DTOs.SolutionDtos.Cloud;
using CloudJ.Contracts.DTOs.SolutionDtos.Photo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.SolutionDtos.Solution
{
    public class UpdateSolutionDto
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PhotoDto[] Photos { get; set; }
        public long? CategoryId { get; set; }
        public NewCloudDto Cloud { get; set; }
    }
}
