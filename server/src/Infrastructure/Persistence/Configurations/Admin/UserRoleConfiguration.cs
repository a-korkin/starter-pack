using Domain.Entities.Admin;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
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
    }
}