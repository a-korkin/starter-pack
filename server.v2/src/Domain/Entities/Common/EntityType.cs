using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using src.Domain.Entities.Base;

namespace src.Domain.Entities.Common
{
    [Table("cs_entity_types", Schema = "common")]
    public class EntityType : BaseEntity
    {
        [Required]
        [Column("c_name")]
        public string Name { get; set; }

        [Required]
        [Column("c_slug")]
        public string Slug { get; set; }

        [Required]
        [Column("c_schema")]
        public string Schema { get; set; }

        [Required]
        [Column("c_tablename")]
        public string TableName { get; set; }
    }
}