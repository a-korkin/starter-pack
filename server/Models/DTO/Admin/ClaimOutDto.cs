using System;

namespace server.Models.DTO.Admin
{
    public class ClaimOutDto 
    {
        public Guid Id { get; set; }

        public Guid TypeId { get; set; }

        public bool Create { get; set; }

        public bool Read { get; set; }

        public bool Update { get; set; }

        public bool Delete { get; set; }
    }
}