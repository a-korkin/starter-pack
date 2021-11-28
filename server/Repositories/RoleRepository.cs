using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.DbContexts;
using server.Entities.Admin;

namespace server.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private readonly ApplicationContext _context;
        public RoleRepository(ApplicationContext context) : base(context) 
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<Role> GetRoleWithChildren(Guid id)
        {
            var role = await _context.Set<Role>()
                .Where(w => w.Id == id)
                .Include(ur => ur.Users)
                .ThenInclude(u => u.User)
                .Include(r => r.Claims)
                .FirstOrDefaultAsync();

            return role;
        }
    }
}