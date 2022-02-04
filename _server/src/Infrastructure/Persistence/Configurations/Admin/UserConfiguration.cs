using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Admin;
using Domain.Entities.Common;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(k => k.Id).HasName("pk_cd_users");

            builder
                .HasOne<Entity>()
                .WithMany()
                .HasForeignKey(p => new { p.Id, p.TypeId })
                .HasConstraintName("fk_cd_users_cd_entities_id");
        }
    }
}