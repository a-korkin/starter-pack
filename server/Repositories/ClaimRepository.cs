using server.DbContexts;
using server.Entities.Admin;

namespace server.Repositories
{
    public class ClaimRepository : GenericRepository<Claim>, IClaimRepository
    {
        public ClaimRepository(ApplicationContext context) : base(context) {}
    }
}