using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO.Admin
{
    public class UserUpdDto
    {
        [Required]
        public string UserName { get; set; }
    }
}