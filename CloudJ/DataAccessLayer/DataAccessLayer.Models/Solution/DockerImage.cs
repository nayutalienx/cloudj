using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models.Solution
{
    public class DockerImage : BaseEntity
    {
        public long CloudId { get; set; }
        public virtual Cloud Cloud { get; set; }
        public string Image { get; set; }
        
    }
}
