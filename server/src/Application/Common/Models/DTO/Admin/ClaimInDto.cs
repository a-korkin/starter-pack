using System;

namespace Application.Common.Models.DTO.Admin
{
    public class ClaimInDto
    {
        public Guid TypeId { get; set; }

        public bool Create { get; set; }

        public bool Read { get; set; }

        public bool Update { get; set; }

        public bool Delete { get; set; }
    }
}