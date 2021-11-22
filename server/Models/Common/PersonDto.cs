using System;

namespace server.Models.Common
{
    public class PersonDto 
    {
        public Guid Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }
    }
}