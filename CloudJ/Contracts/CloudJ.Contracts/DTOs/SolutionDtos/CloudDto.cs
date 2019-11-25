using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.SolutionDtos
{
    public class CloudDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public DockerImageDto Container { get; set; }
    }
}
