using System;
using System.ComponentModel.DataAnnotations;

namespace server.Entities.Common 
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}