using System.Reflection;
using Microsoft.EntityFrameworkCore;
using src.Domain.Entities.Admin;
using src.Domain.Entities.Common;
using src.Infrastructure.Persistence.Configurations;

namespace src.Infrastructure.Persistence
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt) {}

        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}