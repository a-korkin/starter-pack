using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using server.Entities.Base;

namespace server.Entities.Common 
{
    [Table("cd_entities", Schema = "common")]
    public class Entity : BaseModel
    {
        [Column("c_type")]
        [Required]
        public string Type { get; set; }
    }
}