using System;
using System.Collections.Generic;
using server.Entities.Admin;

namespace server.Models.DTO.Admin
{
    public class RoleOutDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public ICollection<UserOutDto> Users { get; set; }
    }
}