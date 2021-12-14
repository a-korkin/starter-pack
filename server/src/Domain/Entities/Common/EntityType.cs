using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Attributes;
using Domain.Entities.Base;

namespace Domain.Entities.Common
{
    [Description(name: "cs_entity_types", schema: "common", ruName: "типы сущностей")]
    public class EntityType : BaseEntity
    {
        [Column("c_name")]
        [Required]
        public string Name { get; set; }

        [Column("c_slug")]
        [Required]
        public string Slug { get; set; }

        [Column("c_schema")]
        [Required]
        public string Schema { get; set; }

        [Column("c_tablename")]
        [Required]
        public string TableName { get; set; }
    }
}