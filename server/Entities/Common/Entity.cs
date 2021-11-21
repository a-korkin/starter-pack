using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using server.Entities.Base;

namespace server.Entities.Common 
{
    [Table("cd_entities", Schema = "common")]
    public class Entity : BaseModel
    {
        // [Key]
        // [Column("id")]
        // public override Guid Id { get; set; }
    }
}