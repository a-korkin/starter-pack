using System;
using System.Threading.Tasks;
using server.Entities.Admin;

namespace server.Repositories
{
    public interface IRoleRepository : IBaseRepository<Role> 
    {
        Task<Role> GetRoleWithChildren(Guid id);
    }
}