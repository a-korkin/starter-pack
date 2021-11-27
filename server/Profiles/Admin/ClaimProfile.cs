using AutoMapper;

namespace server.Profiles.Admin
{
    public class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CreateMap<Models.DTO.Admin.ClaimInDto, Entities.Admin.Claim>();
            CreateMap<Entities.Admin.Claim, Models.DTO.Admin.ClaimOutDto>();
            CreateMap<Models.DTO.Admin.ClaimUpdDto, Entities.Admin.Claim>();
        }
    }
}