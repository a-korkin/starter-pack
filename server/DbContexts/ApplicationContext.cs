using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Entities.Base;
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
            modelBuilder.Entity<Person>(PersonConfigure); 
        }

        private void PersonConfigure(EntityTypeBuilder<Person> builder) 
        {
            builder
                .HasKey(k => k.Id)
                .HasName("pk_cd_persons");

            builder
                .HasOne<Entity>()
                .WithMany()
                .HasForeignKey(p => p.Id)
                .HasConstraintName("fk_cd_persons_cd_entities_id");           
        }
    }
}