using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Entities.Admin
{
    [Table("cd_roles", Schema = "admin")]
    public class Role : AuditedEntity
    {
        [Column("c_title")]
        public string Title { get; set; }

        public ICollection<Claim> Claims { get; set; }
    }
}