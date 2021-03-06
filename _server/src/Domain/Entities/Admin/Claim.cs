using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Attributes;
using Domain.Entities.Base;
using Domain.Entities.Common;

namespace Domain.Entities.Admin
{
    [Description(name: "cd_claims", schema: "admin", ruName: "клэймы")]
    public class Claim : BaseEntity
    {
        [Column("f_type")]
        public Guid TypeId { get; set; }

        public EntityType Type { get; set; }

        [Column("f_role")]
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