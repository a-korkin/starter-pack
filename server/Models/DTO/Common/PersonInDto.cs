using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO.Common 
{
    public class PersonInDto
    {
        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }
    }
}