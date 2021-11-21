using System;
using Microsoft.EntityFrameworkCore;
using server.Entities.Common;

namespace server.DbContexts 
{
    public class ApplicationContext : DbContext 
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt) {}
        
        public DbSet<Entity> Entities { get; set; }
        
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Entity>().HasKey(k => k.Id).HasName("pk_cd_entities");
            modelBuilder.Entity<Person>().HasKey(k => k.Id).HasName("pk_cd_persons");
            
            modelBuilder.Entity<Person>()
                .HasOne<Entity>()
                .WithMany()
                .HasForeignKey(p => p.Id)
                .HasConstraintName("fk_cd_persons_cd_entities_id");
        }
    }
}