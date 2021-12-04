using System;
using System.ComponentModel.DataAnnotations.Schema;
using src.Domain.Entities.Base;

namespace src.Domain.Entities.Common
{
    [Table("cd_entities", Schema = "common")]
    public class Entity : AuditedEntity
    {
        [Column("f_type")]
        public Guid TypeId { get; set; }

        public EntityType Type { get; set; }
    }
}