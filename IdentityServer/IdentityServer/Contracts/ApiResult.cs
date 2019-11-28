using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Contracts
{
    public sealed class ApiResult : IActionResult
    {
        private readonly object _data;

        public ApiResult(object data) => _data = data;

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;

            response.StatusCode = StatusCodes.Status200OK;
            response.ContentType = "application/json";

            if (_data != null)
            {
                var apiResponse = new ApiResponse<object>(_data);
                var json = JsonConvert.SerializeObject(apiResponse);

                await response.WriteAsync(json);
            }
        }
    }
}
