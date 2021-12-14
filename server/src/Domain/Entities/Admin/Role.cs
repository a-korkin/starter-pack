using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Attributes;
using Domain.Entities.Base;

namespace Domain.Entities.Admin
{
    [Description(name: "cd_roles", schema: "admin", ruName: "роли", isEntity: true)]
    public class Role : AuditedEntity
    {
        [Column("c_title")]
        public string Title { get; set; }

        public ICollection<Claim> Claims { get; set; }

        public ICollection<UserRole> Users { get; set; }
    }
}