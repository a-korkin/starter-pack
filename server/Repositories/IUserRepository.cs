using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using server.Entities.Admin;

namespace server.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<Claim>> GetUserClaimsAsync(Guid userId);
    }
}