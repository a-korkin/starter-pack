using AutoMapper;

namespace server.Profiles.Admin
{
    public class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CreateMap<Models.DTO.Admin.ClaimInDto, Entities.Admin.Claim>();
                // .ForMember(
                //     dest => dest.TypeId,
                //     opt => opt.MapFrom(src => src.TypeId)
                // );

            CreateMap<Entities.Admin.Claim, Models.DTO.Admin.ClaimOutDto>();
                // .ForMember(
                //     dest => dest.Type,
                //     opt => opt.MapFrom(src => src.TypeId)
                // );
        }
    }
}