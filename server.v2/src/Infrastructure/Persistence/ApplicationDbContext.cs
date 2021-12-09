using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Admin;
using Domain.Entities.Common;
using Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using Domain.Entities.Base;
using Domain.Attributes;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt) {}

        public DbSet<EntityType> EntityTypes  => Set<EntityType>();
        public DbSet<Entity> Entities { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries<AuditedEntity>().ToArray())
            {
                DescriptionAttribute descriptionAttribute =
                    (DescriptionAttribute)Attribute.GetCustomAttribute(entry.Entity.GetType(), typeof(DescriptionAttribute));

                switch(entry.State)
                {
                    case EntityState.Added:

                        if (descriptionAttribute != null)
                        {
                            var entityType = await EntityTypes
                                .Where(w => w.Schema == descriptionAttribute.Schema)
                                .Where(w => w.TableName == descriptionAttribute.TableName)
                                .FirstOrDefaultAsync();

                            var entity = new Entity
                            {
                                Id = entry.Entity.Id,
                                Type = entityType
                            };
                            await Entities.AddAsync(entity);
                        }
                    break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}