using AutoMapper;

namespace server.Profiles.Admin
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<Entities.Admin.User, Models.DTO.Admin.AuthOutDto>();
        }
    }
}