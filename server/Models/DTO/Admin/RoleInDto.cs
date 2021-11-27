using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO.Admin
{
    public class RoleInDto
    {
        [Required]
        public string Title { get; set; }

        public ICollection<UserRoleInDto> Users { get; set; }
    }
}