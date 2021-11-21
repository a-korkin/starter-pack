using Microsoft.EntityFrameworkCore;
using server.Entities.Common;

namespace server.DbContexts 
{
    public class ApplicationContext : DbContext 
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt) {}

        public DbSet<Entity> Entities { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}