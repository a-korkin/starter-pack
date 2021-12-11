using AutoMapper;

namespace Application.Common.Profiles.Common
{
    public class EntityTypeProfile : Profile
    {
        public EntityTypeProfile()
        {
            CreateMap<Domain.Entities.Common.EntityType, Models.DTO.Common.EntityTypeOutDto>();
        }
    }
}