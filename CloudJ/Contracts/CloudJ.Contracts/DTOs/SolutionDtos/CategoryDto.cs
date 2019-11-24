using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.SolutionDtos.DTOs
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public PhotoDto Logo { get; set; }
        public CategoryDto ParentCategory { get; set; }
    }
}
