using System.Net;
using System.Security.Claims;
using app.Shared;

namespace app.Extensions
{
    public static class HttpContextExtension
    {
        public static int GetUserId(this HttpContext httpContext)
        {
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                throw new HttpResponseException(HttpStatusCode.Unauthorized, "Unauthorized");
            
            return int.Parse(userId);
        }
    }
}