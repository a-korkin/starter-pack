using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Attributes;
using Domain.Entities.Base;

namespace Domain.Entities.Common
{
    [Description(name: "cd_entities", schema: "common", ruName: "сущности")]
    public class Entity: BaseEntity
    {
        [Column("f_type")]
        [Required]
        public Guid TypeId { get; set; }

        public EntityType Type { get; set; }
    }
}