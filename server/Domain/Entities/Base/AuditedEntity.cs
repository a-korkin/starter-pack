using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Base
{
    public class AuditedEntity : BaseEntity
    {
        [Column("f_type")]
        public Guid TypeId { get; set; }
    }
}