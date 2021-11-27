using System.Collections.Generic;

namespace server.Models.DTO.Admin
{
    public class RoleOutItemDto : RoleOutDto
    {
        public ICollection<UserOutDto> Users { get; set; }
    }
}