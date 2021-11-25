using System;
using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO.Admin
{
    public class ClaimInDto
    {
        [Required]
        public Guid TypeId { get; set; }

        [Required]
        public bool Create { get; set; }

        [Required]
        public bool Read { get; set; }

        [Required]
        public bool Update { get; set; }

        [Required]
        public bool Delete { get; set; }
    }
}