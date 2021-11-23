using System;

namespace server.Models.DTO.Admin
{
    public class EntityTypeOutDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Schema { get; set; }

        public string TableName { get; set; }
    }
}