﻿namespace JustDoIt.Model.ViewModels
{
    public class TaskSearchParams
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? IssuerId { get; set; }

        public string? Summary { get; set; }

        public string? Description { get; set; }

        public int? ProjectId { get; set; }

        public string? PictureUrl { get; set; }

        public DateTime? DeadlineStart { get; set; }
        public DateTime? DeadlineEnd { get; set; }

        public DateTime? MinCreatedDate { get; set; }
        public DateTime? MaxCreatedDate { get; set; }

        public bool? IsActive { get; set; }

        public string? State { get; set; }
    }
}
