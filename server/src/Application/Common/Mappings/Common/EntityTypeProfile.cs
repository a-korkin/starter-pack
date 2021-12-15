using AutoMapper;

namespace Application.Common.Mappings.Common
{
    public class EntityTypeProfile : Profile
    {
        public EntityTypeProfile()
        {
            CreateMap<Domain.Entities.Common.EntityType, Models.DTO.Common.EntityTypeOutDto>();
        }
    }
}