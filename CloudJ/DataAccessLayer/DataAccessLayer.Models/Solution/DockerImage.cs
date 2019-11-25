using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models.Solution
{
    public class DockerImage : BaseEntity
    {
        public byte[] Image { get; set; }
    }
}
