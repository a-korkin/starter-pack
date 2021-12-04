using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Admin;
using Domain.Entities.Common;

namespace Infrastructure.Persistence.Configurations
{
    public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            builder.HasKey(k => k.Id).HasName("pk_cd_claims");

            builder.Property(b => b.Create).HasDefaultValue(false);
            builder.Property(b => b.Read).HasDefaultValue(false);
            builder.Property(b => b.Update).HasDefaultValue(false);
            builder.Property(b => b.Delete).HasDefaultValue(false);


            builder
                .HasOne<EntityType>(p => p.Type)
                .WithMany()
                .HasForeignKey(p => p.TypeId)
                .HasConstraintName("fk_cd_claims_cs_entity_types_f_type");

            builder
                .HasOne<Role>(p => p.Role)
                .WithMany(u => u.Claims)
                .HasForeignKey(p => p.RoleId)
                .HasConstraintName("fk_cd_claims_cd_roles_f_role");
        }
    }
}