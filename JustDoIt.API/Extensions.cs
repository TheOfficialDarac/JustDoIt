using System.Security.Claims;

namespace JustDoIt.API
{
    public static class Extensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
            {
                return string.Empty;
            }
            return httpContext.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
