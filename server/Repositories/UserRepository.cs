using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.DbContexts;
using server.Entities.Admin;

namespace server.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context) : base(context) 
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Claim>> GetUserClaimsAsync(Guid userId)
        {
            var claims = await _context.Set<User>()
                .Where(w => w.Id == userId)
                .Include(i => i.Roles)
                .ThenInclude(i => i.Role)
                .ThenInclude(i => i.Claims)
                .ThenInclude(i => i.Type)
                .SelectMany(s => s.Roles.SelectMany(a => a.Role.Claims))
                .ToListAsync();

            return claims;
        }
    }
}