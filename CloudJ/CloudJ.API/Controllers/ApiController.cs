using CloudJ.API.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudJ.API.Controllers
{
    public abstract class ApiController : ControllerBase
    {
        protected ApiResult ApiResult(object @object) =>
            new ApiResult(@object);
    }
}
