using CloudJ.Contracts.DTOs.SolutionDtos.Photo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.SolutionDtos.Category
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PhotoDto Logo { get; set; }
        public CategoryDto ParentCategory { get; set; }
    }
}
