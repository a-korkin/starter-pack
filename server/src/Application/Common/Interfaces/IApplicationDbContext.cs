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

        DbSet<Entity> Entities { get; }

        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}