using IdentityServer.Contracts;
using IdentityServer.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public sealed class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UserReadableException exception)
            {
                // Добавить логирование.
                await SetErrorAsync(context, exception);
                context.Response.StatusCode = StatusCodes.Status200OK;
            }
            catch (Exception exception)
            {
                // Добавить логирование.
                await SetErrorAsync(context, exception);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }

        private static async Task SetErrorAsync(HttpContext context, Exception exception)
        {
            var error = new ApiResponse(exception.Message);
            var json = JsonConvert.SerializeObject(error);

            await context.Response.WriteAsync(json);
        }
    }
}
