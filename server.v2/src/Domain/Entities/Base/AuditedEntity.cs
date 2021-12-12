using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Base
{
    public abstract class AuditedEntity : BaseEntity
    {
        [Column("f_type")]
        public Guid TypeId { get; set; }
    }
}