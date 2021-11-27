using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using server.Attributes;
using server.Entities.Base;

namespace server.Entities.Admin
{
    [Table("cd_user_roles", Schema = "admin")]
    public class UserRole : BaseEntity
    {
        [Key]
        [Column("id")]
        // [ForeignKey("fk_cd_user_roles_cd_entities_id")]
        public override Guid Id { get; set; } = Guid.NewGuid();

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