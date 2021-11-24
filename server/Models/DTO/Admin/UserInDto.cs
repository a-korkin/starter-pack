using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO.Admin
{
    public class UserInDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}