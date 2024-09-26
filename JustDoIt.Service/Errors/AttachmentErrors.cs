using JustDoIt.Common;

namespace JustDoIt.Service.Errors
{
    public static class AttachmentErrors
    {
        public static readonly Error NotFound = new("404", "No Attachment not found.");
        public static readonly Error BadRequest = new("400", "Bad request.");

    }
}
