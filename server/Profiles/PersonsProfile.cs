using AutoMapper;

namespace server.Profiles
{
    public class PersonsProfile : Profile
    {
        public PersonsProfile()
        {
            CreateMap<Entities.Common.Person, Models.PersonDto>();
            CreateMap<Models.PersonCreateDto, Entities.Common.Person>();
        }
    }
}