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
            return httpContext.User.Claims.Single(x => x.Type == "sub").Value;
        }
    }
}
