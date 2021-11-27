using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO.Admin
{
    public class ClaimUpdDto
    {
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