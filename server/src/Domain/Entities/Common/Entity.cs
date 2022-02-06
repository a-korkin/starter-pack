using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Attributes;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Common
{
    [Description(name: "cd_entities", schema: "common", ruName: "сущности")]
    [Comment("сущности")]
    public class Entity: BaseEntity
    {
        [Column("f_type")]
        [Comment("тип")]
        [Required]
        public Guid TypeId { get; set; }

        public EntityType Type { get; set; }
    }
}