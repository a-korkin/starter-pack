using System.ComponentModel.DataAnnotations;

namespace server.Models.Admin
{
    public class EntityTypeInDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public string Schema { get; set; }

        [Required]
        public string TableName { get; set; }
    }
}