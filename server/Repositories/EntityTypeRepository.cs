using server.DbContexts;
using server.Entities.Admin;

namespace server.Repositories
{
    public class EntityTypeRepository : GenericRepository<EntityType>, IEntityTypeRepository
    {
        public EntityTypeRepository(ApplicationContext context) : base(context) {}
    }
}