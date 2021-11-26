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
        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<EntityType>().HasKey(k => k.Id).HasName("pk_cs_entity_types");
            modelBuilder.Entity<Entity>(EntityConfigure);
            modelBuilder.Entity<User>(UserConfigure);
            modelBuilder.Entity<Claim>(ClaimConfigure);
            modelBuilder.Entity<Role>().HasKey(k => k.Id).HasName("pk_cd_roles");
            modelBuilder.Entity<UserRole>(UserRoleConfigure);  
            modelBuilder.Entity<Person>(PersonConfigure); 
        }

        private void EntityConfigure(EntityTypeBuilder<Entity> builder)
        {
            builder
                .HasKey(k => k.Id)
                .HasName("pk_cd_entities");

            builder
                .HasOne<EntityType>(p => p.Type)
                .WithMany()
                .HasForeignKey(p => p.TypeId)
                .HasConstraintName("fk_cd_entities_cs_entitie_types_id");
        }

        private void UserConfigure(EntityTypeBuilder<User> builder) 
        {
            builder
                .HasKey(k => k.Id)
                .HasName("pk_cd_users");

            builder
                .HasOne<Entity>()
                .WithMany()
                .HasForeignKey(p => p.Id)
                .HasConstraintName("fk_cd_users_cd_entities_id");
        }

        private void UserRoleConfigure(EntityTypeBuilder<UserRole> builder)
        {
            builder
                .HasKey(uk => new { uk.UserId, uk.RoleId })
                .HasName("pk_cd_user_claims");;

            builder
                .HasOne(uk => uk.User)
                .WithMany(k => k.Roles)
                .HasForeignKey(uk => uk.UserId)
                .HasConstraintName("fk_cd_user_roles_cd_roles_f_user");

            builder
                .HasOne(ur => ur.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.RoleId)
                .HasConstraintName("fk_cd_user_roles_cd_users_f_role");   
        }

        private void ClaimConfigure(EntityTypeBuilder<Claim> builder) 
        {
            builder
                .HasKey(k => k.Id)
                .HasName("pk_cd_claims");

            builder.Property(b => b.Create).HasDefaultValue(false);
            builder.Property(b => b.Read).HasDefaultValue(false);
            builder.Property(b => b.Update).HasDefaultValue(false);
            builder.Property(b => b.Delete).HasDefaultValue(false);

            builder
                .HasOne<Entity>()
                .WithMany()
                .HasForeignKey(p => p.Id)
                .HasConstraintName("fk_cd_claims_cd_entities_id");

            builder
                .HasOne<EntityType>(p => p.Type)
                .WithMany()
                .HasForeignKey(p => p.TypeId)
                .HasConstraintName("fk_cd_claims_cs_entity_types_f_type");

            builder
                .HasOne<Role>(p => p.Role)
                .WithMany()
                .HasForeignKey(p => p.RoleId)
                .HasConstraintName("fk_cd_claims_cd_roles_f_role");
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