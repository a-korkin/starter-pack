using System;
using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO.Admin
{
    public class UserRoleInDto
    {
        [Required]
        public Guid UserId { get; set; }
    }
}