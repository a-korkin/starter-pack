using System;

namespace server.Models.DTO.Common
{
    public class PersonOutDto 
    {
        public Guid Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }
    }
}