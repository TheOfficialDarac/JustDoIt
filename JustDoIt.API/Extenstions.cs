using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace JustDoIt.API
{
    public static class Extenstions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
            {
                return string.Empty;
            }
            return httpContext.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            //return await httpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "");
        }
    }
}
