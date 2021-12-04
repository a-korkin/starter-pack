using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using src.Domain.Entities.Base;

namespace src.Domain.Entities.Admin
{
    [Table("cd_roles", Schema = "admin")]
    public class Role : AuditedEntity
    {
        [Column("c_title")]
        public string Title { get; set; }

        public ICollection<Claim> Claims { get; set; }
    }
}