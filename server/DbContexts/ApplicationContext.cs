using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Entities.Admin;
using server.Entities.Common;

namespace server.DbContexts 
{
    public class ApplicationContext : DbContext 
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt) {}
        
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<EntityType>().HasKey(k => k.Id).HasName("pk_cs_entity_types");
            modelBuilder.Entity<Entity>().HasKey(k => k.Id).HasName("pk_cd_entities");
            modelBuilder.Entity<Entity>()
                .HasOne<EntityType>(p => p.Type)
                .WithMany()
                .HasForeignKey(p => p.TypeId)
                .HasConstraintName("fk_cd_entities_cs_entitie_types_id");

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