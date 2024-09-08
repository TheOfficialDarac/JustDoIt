using JustDoIt.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Service.Errors
{
    public static class TaskErrors
    {
        //public static readonly Error SameUser = new("Followers.SameUser", "Can't follow yourself");

        //public static readonly Error NonPublicProfile = new(
        //    "Followers.NonPublicProfile",
        //    "Can't follow non-public profiles");

        //public static readonly Error AlreadyFollowing = new(
        //    "Followers.AlreadyFollowing",
        //    "Already following");

        public static readonly Error Test = new Error("Tasks.Test", "This is a test errror");
        public static readonly Error NotFound = new Error("404", "Task not found.");

    }
}
