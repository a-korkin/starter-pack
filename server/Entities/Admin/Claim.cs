using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using server.Entities.Base;

namespace server.Entities.Admin
{
    [Table("cd_claims", Schema = "admin")]
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

        public ICollection<UserClaim> UserClaims { get; set; }
    }
}