using AutoMapper;

namespace Application.Common.Profiles.Admin
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<Domain.Entities.Admin.User, Models.DTO.Admin.AuthOutDto>();
        }
    }
}