using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        [Key]
        [Column("id")]
        [Comment("идентификатор")]
        public virtual Guid Id { get; set; } = Guid.NewGuid();
    }
}