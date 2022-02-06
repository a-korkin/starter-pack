using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Base
{
    public class AuditedEntity : BaseEntity
    {
        [Column("f_type")]
        [Comment("тип")]
        public Guid TypeId { get; set; }
    }
}