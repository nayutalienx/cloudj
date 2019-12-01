using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudJ.Client.Models
{
    public class NewCollectionModel
    {
        public string Name { get; set; }
        public string Preview { get; set; }
        public string Description { get; set; }
        public IFormFile PhotoPreview { get; set; }
    }
}
