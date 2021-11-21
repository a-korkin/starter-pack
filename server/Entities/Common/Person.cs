using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using server.Entities.Base;

namespace server.Entities.Common
{
    [Table("cd_persons", Schema = "common")]
    public class Person : BaseModel
    {
        [Column("id")]
        [Key]
        [ForeignKey("fk_cd_persons_cd_entities_id")]
        public override Guid Id { get; set; }

        [Column("c_last_name")]
        [Required]
        public string LastName { get; set; }

        [Column("c_first_name")]
        [Required]
        public string FirstName { get; set; }
    }
}