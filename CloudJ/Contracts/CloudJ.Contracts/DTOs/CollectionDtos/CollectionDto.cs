﻿using CloudJ.Contracts.DTOs.SolutionDtos.Photo;
using CloudJ.Contracts.DTOs.SolutionDtos.Solution;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.CollectionDtos
{
    public class CollectionDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Preview { get; set; }
        public string Description { get; set; }
        public TruncSolutionDto[] Solutions { get; set; }
        public PhotoDto PhotoPreview { get; set; }
    }
}
