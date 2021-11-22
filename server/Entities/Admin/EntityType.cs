using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using server.Entities.Base;

namespace server.Entities.Admin
{
    [Table("cs_entity_types", Schema = "admin")]
    public class EntityType : BaseModel
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