using System;

namespace server.Models.DTO.Admin
{
    public class AuthOutDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string AccessToken { get; set; }
    } 
}