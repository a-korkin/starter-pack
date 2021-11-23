using AutoMapper;

namespace server.Profiles.Common
{
    public class PersonsProfile : Profile
    {
        public PersonsProfile()
        {
            CreateMap<Entities.Common.Person, Models.DTO.Common.PersonOutDto>();
            CreateMap<Models.DTO.Common.PersonInDto, Entities.Common.Person>();
        }
    }
}