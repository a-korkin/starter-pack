using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using server.Attributes;
using server.Entities.Base;

namespace server.Entities.Admin
{
    [Table("cd_claims", Schema = "admin")]
    [Description(name: "клэймы", slug: "claims", schema: "admin", tableName: "cd_claims")]
    public class Claim : BaseEntity
    {
        [Key]
        [Column("id")]
        [ForeignKey("fk_cd_claims_cd_entities_id")]
        public override Guid Id { get; set; }

        [Column("f_type")]
        [Required]
        public Guid TypeId { get; set; }

        public EntityType Type { get; set; }

        [Column("f_role")]
        [Required]
        public Guid RoleId { get; set; }

        public Role Role { get; set; }

        [Column("b_create")]
        [Required]
        public bool Create { get; set; }

        [Column("b_read")]
        [Required]
        public bool Read { get; set; }

        [Column("b_update")]
        [Required]
        public bool Update { get; set; }

        [Column("b_delete")]
        [Required]
        public bool Delete { get; set; }
    }
}