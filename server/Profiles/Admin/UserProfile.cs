using AutoMapper;

namespace server.Profiles.Admin
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Models.DTO.Admin.UserInDto, Entities.Admin.User>();
            CreateMap<Entities.Admin.User, Models.DTO.Admin.UserOutDto>();
        }
    }
}