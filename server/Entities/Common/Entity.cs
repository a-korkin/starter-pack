using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using server.Entities.Admin;
using server.Entities.Base;

namespace server.Entities.Common 
{
    [Table("cd_entities", Schema = "common")]
    public class Entity : BaseEntity
    {
        [Column("f_type")]
        public Guid TypeId { get; set; }

        public EntityType Type { get; set; }
    }
}