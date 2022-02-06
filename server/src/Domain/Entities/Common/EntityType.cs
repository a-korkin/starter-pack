using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Attributes;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Common
{
    [Description(name: "cs_entity_types", schema: "common", ruName: "типы сущностей")]
    [Comment("типы сущностей")]
    public class EntityType : BaseEntity
    {
        [Column("c_name")]
        [Comment("название")]
        [Required]
        public string Name { get; set; }

        [Column("c_slug")]
        [Comment("код")]
        [Required]
        public string Slug { get; set; }

        [Column("c_schema")]
        [Comment("схема")]
        [Required]
        public string Schema { get; set; }

        [Column("c_tablename")]
        [Comment("таблица")]
        [Required]
        public string TableName { get; set; }
    }
}