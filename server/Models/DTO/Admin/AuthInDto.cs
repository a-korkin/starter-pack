using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO.Admin
{
    public class AuthInDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}