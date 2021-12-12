using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Entities.Common
{
    [Table("cd_entities", Schema = "common")]
    public class Entity : BaseEntity
    {
        [Column("f_type")]
        public Guid TypeId { get; set; }

        public EntityType Type { get; set; }
    }
}