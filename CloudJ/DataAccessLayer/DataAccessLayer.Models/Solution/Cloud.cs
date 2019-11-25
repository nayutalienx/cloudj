using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models.Solution
{
    public class Cloud : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public long ContainerId { get; set; }
        public virtual DockerImage Container { get; set; }
    }
}
