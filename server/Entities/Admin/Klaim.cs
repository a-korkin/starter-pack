using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using server.Entities.Base;

namespace server.Entities.Admin
{
    [Table("cd_claims", Schema = "admin")]
    public class Klaim : BaseEntity
    {
        [Key]
        [Column("id")]
        [ForeignKey("fk_cd_claims_cd_entities_id")]
        public override Guid Id { get; set; }

        [Column("c_type")]
        [Required]
        public string Type { get; set; }

        [Column("c_value")]
        [Required]
        public string Value { get; set; }

        public ICollection<UserKlaim> UserKlaims { get; set; }
    }
}