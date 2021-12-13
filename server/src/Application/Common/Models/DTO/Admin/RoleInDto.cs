using System.Collections.Generic;

namespace Application.Common.Models.DTO.Admin
{
    public class RoleInDto
    {
        public string Title { get; set; }

        public ICollection<UserRoleInDto> Users { get; set; }

        public ICollection<ClaimInDto> Claims { get; set; }
    }
}