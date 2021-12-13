using AutoMapper;

namespace Application.Common.Profiles.Admin
{
    public class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CreateMap<Models.DTO.Admin.ClaimInDto, Domain.Entities.Admin.Claim>();
            CreateMap<Domain.Entities.Admin.Claim, Models.DTO.Admin.ClaimOutDto>();
            CreateMap<Models.DTO.Admin.ClaimUpdDto, Domain.Entities.Admin.Claim>();
        }
    }
}