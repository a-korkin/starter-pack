using AutoMapper;

namespace Application.Common.Mappings.Admin
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Models.DTO.Admin.UserInDto, Domain.Entities.Admin.User>();
            CreateMap<Domain.Entities.Admin.User, Models.DTO.Admin.UserOutDto>();
        }
    }
}