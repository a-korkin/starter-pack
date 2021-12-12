using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Admin;
using Domain.Entities.Common;

namespace Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(k => k.Id).HasName("pk_cd_roles");

            builder
                .HasOne<Entity>()
                .WithMany()
                .HasForeignKey(p => new { p.Id, p.TypeId })
                .HasConstraintName("fk_cd_roles_cd_entities_id");
        }
    }
}