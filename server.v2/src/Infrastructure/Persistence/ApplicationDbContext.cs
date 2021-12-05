using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Admin;
using Domain.Entities.Common;
using Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt) {}

        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            // foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            // {
            //     switch (entry.State)
            //     {
            //         case EntityState.Added:
            //             entry.Entity.CreatedBy = _currentUserService.UserId;
            //             entry.Entity.Created = _dateTime.Now;
            //             break;

            //         case EntityState.Modified:
            //             entry.Entity.LastModifiedBy = _currentUserService.UserId;
            //             entry.Entity.LastModified = _dateTime.Now;
            //             break;
            //     }
            // }

            // var events = ChangeTracker.Entries<IHasDomainEvent>()
            //         .Select(x => x.Entity.DomainEvents)
            //         .SelectMany(x => x)
            //         .Where(domainEvent => !domainEvent.IsPublished)
            //         .ToArray();

            var result = await base.SaveChangesAsync(cancellationToken);

            // await DispatchEvents(events);

            return result;
        }
    }
}