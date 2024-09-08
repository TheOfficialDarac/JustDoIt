using JustDoIt.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Service.Errors
{
    public static class ProjectErrors
    {
        public static readonly Error NotFound = new Error("404", "Task not found.");
    }
}
