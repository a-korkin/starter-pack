using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using src.Domain.Entities.Base;

namespace src.Domain.Entities.Admin
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
        [Required]
        public string RefreshToken { get; set; }
    }
}