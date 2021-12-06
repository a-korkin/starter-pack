using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Entities.Admin
{
    [Table("cd_users", Schema = "admin")]
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
    }
}