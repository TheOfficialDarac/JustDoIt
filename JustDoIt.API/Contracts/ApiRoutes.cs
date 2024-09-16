using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace JustDoIt.API.Contracts
{
    public static class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";
        private const string Base = $"{Root}/{Version}";

        public static class Tasks
        {
            public const string Controller = $"{Base}/tasks";

            public const string GetAll = "";
            public const string Get = "{taskId}";
            public const string Create = "create";
            public const string Delete = "delete";
            public const string Update = "update";

            public const string UserTasks = "user";
            public const string UserProjectTasks = "project/{projectId}";
        }

        public static class Projects
        {
            public const string Controller = $"{Base}/projects";

            public const string GetAll = "";
            public const string Get = "{projectId}";
            public const string Create = "create";
            public const string Delete = "delete";
            public const string Update = "update";

            public const string UserProjects = "user";
        }
        public static class Auth
        {
            public const string Controller = $"{Base}/auth";

            public const string Register = "register";
            public const string Login = "login";
            public const string Logout = "logout";
            public const string Test = "test";
        }
    }
}
