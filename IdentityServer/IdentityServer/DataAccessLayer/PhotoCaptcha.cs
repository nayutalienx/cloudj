using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.DataAccessLayer
{
    public class PhotoCaptcha : BaseEntity
    {
        public byte[] Data { get; set; }
        public string Answer { get; set; }
    }
}
