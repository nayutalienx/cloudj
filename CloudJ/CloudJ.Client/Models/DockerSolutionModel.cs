using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudJ.Client.Models
{
    public class DockerSolutionModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public IFormFile Logo { get; set; }
        public List<IFormFile> Photos { get; set; }
        public long CategoryId { get; set; }
        public string CloudName { get; set; }
        public string DockerUrl { get; set; }
    }
}
