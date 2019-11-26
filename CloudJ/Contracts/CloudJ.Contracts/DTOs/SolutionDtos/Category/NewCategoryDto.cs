using CloudJ.Contracts.DTOs.SolutionDtos.Photo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.SolutionDtos.Category
{
    public class NewCategoryDto
    {
        public string Name { get; set; }
        public long? ParentCategoryId { get; set; }
        public NewPhotoDto Logo { get; set; }
    }
}
