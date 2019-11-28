using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Objects
{
    public class CaptchaModelView
    {
        public IFormFile Photo { get; set; }
        public string Answer { get; set; }
    }
}
