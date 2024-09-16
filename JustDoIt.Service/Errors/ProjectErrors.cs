using JustDoIt.Common;

namespace JustDoIt.Service.Errors
{
    public static class ProjectErrors
    {
        public static readonly Error NotFound = new("404", "Task not found.");
        public static readonly Error BadRequest = new("400", "Bad Request.");
    }
}
