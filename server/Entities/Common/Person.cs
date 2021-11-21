using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using server.Entities.Base;
using server.Attributes;

namespace server.Entities.Common
{
    [Table("cd_persons", Schema = "common")]
    [Fuck(name: "Person")]
    public class Person : BaseModel
    {
        [Key]
        [Column("id")]
        [ForeignKey("fk_cd_persons_cd_entities_id")]
        public override Guid Id { get; set; }

        [Required]
        [Column("c_last_name")]
        public string LastName { get; set; }

        [Required]
        [Column("c_first_name")]
        public string FirstName { get; set; }
    }
}