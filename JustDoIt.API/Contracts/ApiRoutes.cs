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
            public const string UserProjectTasks = "project";
        }

        public static class Projects
        {
            public const string Controller = $"{Base}/projects";

            public const string GetAll = "";
            public const string Get = "{projectId}";
            public const string Create = "create";
            public const string Delete = "delete";
            public const string Update = "update";

            public const string UserProjects = "user/{userId}";
            public const string CurrentUserProjects = "current-user";
            public const string CurrentUserProjectRole = "user-role";
        }

        public static class Auth
        {
            public const string Controller = $"{Base}/auth";

            public const string Register = "register";
            public const string Login = "login";
            public const string Logout = "logout";
            public const string VerifyEmail = "verify-email";
            public const string UserData = "data";
            public const string Update = "update";

            public const string Test = "test";
        }

        public static class Attachments
        {
            public const string Controller = $"{Base}/attachments";

            public const string Get = "{attachmentId}";

            public const string GetAllTasks = "tasks/";
            public const string CreateTasks = "tasks/create";
            public const string DeleteTasks = "tasks/delete";
            public const string UpdateTasks = "tasks/update";

            public const string GetAllIssues = "issues/";
            public const string CreateIssues = "issues/create";
            public const string DeleteIssues = "issues/delete";
            public const string UpdateIssues = "issues/update";
        }

        public static class Utils
        {
            public const string Controller = $"{Base}/utils";

            public const string GetAllCategories = "categories";
            public const string GetAllProjectCategories = "categories/{projectId}";

            public const string GetAllStatuses = "statuses";
            public const string GetProjectStatus = "statuses/{projectId}";

            public const string GetAllStates = "states";
            public const string GetTaskState = "states/{taskId}";
        }

        public static class Comments
        {
            public const string Controller = $"{Base}/comments";

            public const string GetAll = "";
            public const string Get = "{commentId}";
            public const string Create = "create";
            public const string Delete = "delete";
            public const string Update = "update";
        }
    }
}
