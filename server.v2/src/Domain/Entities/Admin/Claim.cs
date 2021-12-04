using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using src.Domain.Entities.Base;
using src.Domain.Entities.Common;

namespace src.Domain.Entities.Admin
{
    [Table("cd_claims", Schema = "admin")]
    public class Claim : BaseEntity
    {
        [Column("f_type")]
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
    }
}