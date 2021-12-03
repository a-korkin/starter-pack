using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Base 
{
    public abstract class BaseEntity
    {
        [Key]
        [Column("id")]
        public virtual Guid Id { get; set; } = Guid.NewGuid();
    }
}