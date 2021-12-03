using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Entities.Admin
{
    [Table("cd_users", Schema = "admin")]
    public class User : BaseEntity
    {
        [Key]
        [Column("id")]
        public override Guid Id { get; set; }

        [Column("c_username")]
        [Required]
        public string UserName { get; set; }

        [Column("c_password")]
        [Required]
        public string Password { get; set; }

        [Column("c_refresh_token")]
        [Required]
        public string RefreshToken { get; set; }
    }
}