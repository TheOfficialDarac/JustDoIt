﻿using JustDoIt.Model.DTOs.Requests.Abstractions;

namespace JustDoIt.Model.DTOs.Requests.Tasks
{
    public class CreateTaskRequest : CreateRequest
    {
        public string? Title { get; set; }

        public string? Summary { get; set; }

        public string? Description { get; set; }

        public int ProjectId { get; set; }

        public Guid IssuerId { get; set; }

        public string? PictureUrl { get; set; }

        public DateTime? Deadline { get; set; }
    }
}