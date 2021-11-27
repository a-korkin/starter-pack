using System.Linq;
using AutoMapper;

namespace server.Profiles.Admin
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Models.DTO.Admin.RoleInDto, Entities.Admin.Role>();
            CreateMap<Entities.Admin.Role, Models.DTO.Admin.RoleOutDto>();
            CreateMap<Entities.Admin.Role, Models.DTO.Admin.RoleOutItemDto>()
                .ForMember(
                    d => d.Users,
                    o => o.MapFrom(s => s.Users.Select(x => x.User))
                );
        }
    }
}