using IdentityServer.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public abstract class ApiController : Controller
    {
        protected ApiResult ApiResult(object @object) =>
            new ApiResult(@object);
    }
}
