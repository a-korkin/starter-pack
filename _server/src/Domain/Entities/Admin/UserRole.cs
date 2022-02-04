using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Attributes;
using Domain.Entities.Base;

namespace Domain.Entities.Admin
{
    [Description(name: "cd_user_roles", schema: "admin", ruName: "пользователи-роли")]
    public class UserRole : BaseEntity
    {
        [Column("f_user")]
        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; }

        [Column("f_role")]
        [Required]
        public Guid RoleId { get; set; }

        public Role Role { get; set; }
    }
}