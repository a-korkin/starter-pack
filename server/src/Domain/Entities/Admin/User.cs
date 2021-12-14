using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Attributes;
using Domain.Entities.Base;

namespace Domain.Entities.Admin
{
    [Description(name: "cd_users", schema: "admin", slug: "users", ruName: "пользователи", isEntity: true)]
    public class User : AuditedEntity 
    {
        [Column("c_username")]
        [Required]
        public string UserName { get; set; }

        [Column("c_password")]
        [Required]
        public string Password { get; set; }

        [Column("c_refresh_token")]
        public string RefreshToken { get; set; }

        public ICollection<UserRole> Roles { get; set; }
    }
}