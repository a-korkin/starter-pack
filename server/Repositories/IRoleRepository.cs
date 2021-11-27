using System;
using System.Threading.Tasks;
using server.Entities.Admin;

namespace server.Repositories
{
    public interface IRoleRepository 
    {
        Task<Role> GetRoleWithChildren(Guid id);
    }
}