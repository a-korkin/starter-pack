using System.Linq;
using AutoMapper;

namespace Application.Common.Mappings.Admin
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Models.DTO.Admin.RoleInDto, Domain.Entities.Admin.Role>();
            CreateMap<Domain.Entities.Admin.Role, Models.DTO.Admin.RoleOutDto>();
        }
    }
}