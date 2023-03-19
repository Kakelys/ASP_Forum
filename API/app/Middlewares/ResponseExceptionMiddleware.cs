using app.Shared;

namespace app.Middlewares
{
    public class ResponseExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ResponseExceptionMiddleware(RequestDelegate next, ILogger<ResponseExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpResponseException ex)
            {
                _logger.LogWarning(
                    @$"Http Response Exception:
                    \r\nmessage: {ex.Message} 
                    \r\nurl: {context.Request.Path}
                    \r\nmethod:{context.Request.Method}"
                );

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)ex.StatusCode;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}