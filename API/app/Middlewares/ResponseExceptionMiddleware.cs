using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Shared;

namespace app.Middlewares
{
    public class ResponseExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpResponseException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)ex.StatusCode;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}