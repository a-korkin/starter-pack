using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using server.Entities.Base;

namespace server.Entities.Admin
{
    [Table("cd_users", Schema = "admin")]
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
    }
}