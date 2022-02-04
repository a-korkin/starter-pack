using System;

namespace Application.Common.Models.DTO.Common
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