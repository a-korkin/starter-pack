using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using server.Attributes.Validation;

namespace server.Models.DTO.Admin
{
    [RoleValidation]
    public class RoleInDto
    {
        [Required]
        public string Title { get; set; }

        public ICollection<UserRoleInDto> Users { get; set; }

        public ICollection<ClaimInDto> Claims { get; set; }
    }
}