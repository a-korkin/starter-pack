using System.Collections.Generic;

namespace Application.Common.Models.DTO.Admin
{
    public class RoleOutItemDto : RoleOutDto
    {
        public ICollection<UserOutDto> Users { get; set; }

        public ICollection<ClaimOutDto> Claims { get; set; }
    }
}