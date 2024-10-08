﻿using Microsoft.AspNetCore.Http;

namespace JustDoIt.Model.Responses.Auth
{
    public class UpdateUserRequest
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        //public string PictureUrl { get; set; } = string.Empty;
        public IFormFile? Picture { get; set; } = null;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
