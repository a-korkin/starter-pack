using AutoMapper;

namespace server.Profiles.Admin
{
    public class EntityTypeProfile : Profile
    {
        public EntityTypeProfile()
        {
            CreateMap<Models.DTO.Admin.EntityTypeInDto, Entities.Admin.EntityType>();
            CreateMap<Entities.Admin.EntityType, Models.DTO.Admin.EntityTypeOutDto>();
        }
    }
}