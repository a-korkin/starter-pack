using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using server.Attributes;
using server.Entities.Base;

namespace server.Entities.Admin
{
    [Table("cd_roles", Schema = "admin")]
    [Description(name: "роли", slug: "roles", schema: "admin", tableName: "cd_roles")]
    public class Role : BaseEntity
    {
        [Key]
        [Column("id")]
        [ForeignKey("fk_cd_roles_cd_entities_id")]
        public override Guid Id { get; set; }

        [Required]
        [Column("c_title")]
        public string Title { get; set; }
        
        public ICollection<UserRole> Users { get; set; }
    }
}