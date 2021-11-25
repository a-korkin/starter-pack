using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using server.Entities.Base;

namespace server.Entities.Admin
{
    [Table("cd_user_claims", Schema = "admin")]
    public class UserKlaim : BaseEntity
    {
        [Key]
        [Column("id")]
        [ForeignKey("fk_cd_user_claims_cd_entities_id")]
        public override Guid Id { get; set; }

        [Column("f_user")]
        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; }

        [Column("f_claim")]
        [Required]
        public Guid KlaimId { get; set; }

        public Klaim Klaim { get; set; }
    }
}