using JustDoIt.Model.DTOs.Responses.Abstractions;

namespace JustDoIt.Model.DTOs.Responses.Tasks
{
    public class TaskAttachmentResponse:Response
    {
        public int Id { get; set; } = 0;

        public int TaskId { get; set; } = 0;

        public string Filepath { get; set; } = string.Empty;
    }
}
