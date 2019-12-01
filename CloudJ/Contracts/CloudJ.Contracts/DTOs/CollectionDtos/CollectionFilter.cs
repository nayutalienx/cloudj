using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs.CollectionDtos
{
    public class CollectionFilter
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Preview { get; set; }
        public string Description { get; set; }
        public int Size { get; set; } = 5;
        public int Page { get; set; } = 1;
    }
}
