using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Admin;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext 
    {
        DbSet<EntityType> EntityTypes { get; }
        DbSet<User> Users { get; }
        DbSet<Claim> Claims { get; }
        DbSet<Role> Roles { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}