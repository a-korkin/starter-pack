using System.ComponentModel.DataAnnotations;

namespace server.Entities.Common
{
    public class Person : Entity
    {
        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }
    }
}