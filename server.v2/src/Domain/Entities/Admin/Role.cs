using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Attributes;
using Domain.Entities.Base;

namespace Domain.Entities.Admin
{
    [Table("cd_roles", Schema = "admin")]
    [Description(name: "роли", slug: "roles", schema: "admin", tableName: "cd_roles")]
    public class Role : AuditedEntity
    {
        [Column("c_title")]
        public string Title { get; set; }

        public ICollection<Claim> Claims { get; set; }

        public ICollection<UserRole> Users { get; set; }
    }
}