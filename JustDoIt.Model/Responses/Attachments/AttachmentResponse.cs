﻿using JustDoIt.Model.Responses.Abstractions;

namespace JustDoIt.Model.Responses.Attachments
{
    public class AttachmentResponse : Response
    {
        public int Id { get; set; } = 0;

        public int TaskId { get; set; } = 0;

        public string Filepath { get; set; } = string.Empty;
    }
}