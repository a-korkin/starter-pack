using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Entities.Admin
{
    [Table("cd_roles", Schema = "admin")]
    public class Role : BaseEntity
    {
        [Key]
        [Column("id")]
        public override Guid Id { get; set; }

        public ICollection<Claim> Claims { get; set; }
    }
}