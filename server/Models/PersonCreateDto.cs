using System.ComponentModel.DataAnnotations;

namespace server.Models 
{
    public class PersonCreateDto
    {
        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }
    }
}