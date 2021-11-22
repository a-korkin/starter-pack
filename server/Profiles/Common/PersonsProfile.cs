using AutoMapper;

namespace server.Profiles.Common
{
    public class PersonsProfile : Profile
    {
        public PersonsProfile()
        {
            CreateMap<Entities.Common.Person, Models.Common.PersonDto>();
            CreateMap<Models.Common.PersonCreateDto, Entities.Common.Person>();
        }
    }
}