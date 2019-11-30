using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudJ.Client.Models
{
    public class NewCategoryModel
    {
        public string Name { get; set; }
        public long? ParentCategoryId { get; set; }
        public string Description { get; set; }
        public IFormFile Logo { get; set; }
       
    }
}
