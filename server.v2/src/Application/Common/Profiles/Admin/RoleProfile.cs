using System.Linq;
using AutoMapper;

namespace Application.Common.Profiles.Admin
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Models.DTO.Admin.RoleInDto, Domain.Entities.Admin.Role>();
            CreateMap<Domain.Entities.Admin.Role, Models.DTO.Admin.RoleOutDto>();
            CreateMap<Domain.Entities.Admin.Role, Models.DTO.Admin.RoleOutItemDto>()
                .ForMember(
                    d => d.Users,
                    o => o.MapFrom(s => s.Users.Select(x => x.User))
                );
        }
    }
}