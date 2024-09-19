using JustDoIt.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Service.Errors
{
    public static class UserErrors
    {
        public static readonly Error BadRequest = new("400", "Bad Request.");
        public static readonly Error UserExists = new("400", "User with this email address already exists.");
        public static readonly Error NotFound = new("404", "User with this email doesn't exist or has not yet been verified.");
        public static readonly Error InvalidCredentials = new("400", "Email or Password Incorrect.");
        public static readonly Error UserIdNotSet = new("404", "User Id not found. Try loggin in again.");

    }
}
