using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Entities.Base 
{
    public abstract class BaseModel 
    {
        [Key]
        [Column("id")]
        public virtual Guid Id { get; set; }
    }
}