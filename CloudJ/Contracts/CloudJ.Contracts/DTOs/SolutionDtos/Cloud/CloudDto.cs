﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.SolutionDtos.Cloud
{
    public class CloudDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DockerImageDto Container { get; set; }
    }
}
