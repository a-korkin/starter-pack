using System;
using System.Threading.Tasks;
using server.Entities.Admin;

namespace server.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role> 
    {
        Task<Role> GetRoleWithChildren(Guid id);
    }
}