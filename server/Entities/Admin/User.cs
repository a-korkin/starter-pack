using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using server.Attributes;
using server.Entities.Base;

namespace server.Entities.Admin
{
    [Table("cd_users", Schema = "admin")]
    [Description(name: "пользователи", slug: "users", schema: "admin", tableName: "cd_users")]
    public class User : BaseEntity
    {
        [Key]
        [Column("id")]
        [ForeignKey("fk_cd_persons_cd_entities_id")]
        public override Guid Id { get; set; }

        [Required]
        [Column("c_username")]
        public string UserName { get; set; }

        [Required]
        [Column("c_password")]
        public string Password { get; set; }

        public ICollection<UserKlaim> UserKlaims { get; set; }
    }
}