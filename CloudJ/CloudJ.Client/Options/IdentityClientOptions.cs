using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudJ.Client.Options
{
    public class IdentityClientOptions
    {
        public string BaseUrl { get; set; }
        public string UserInfoByTokenUrl { get; set; }
        public string UserInfoByIdUrl { get; set; }
        public string UpdateUserInfoUrl { get; set; }

    }
}
