using AutoMapper;

namespace server.Profiles.Admin
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<Models.DTO.Admin.UserRoleInDto, Entities.Admin.UserRole>();
        }
    }
}