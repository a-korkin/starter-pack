using Domain.Entities.Admin;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Admin
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(k => k.Id).HasName("pk_cd_users");

            builder
                .HasOne<Entity>()
                .WithMany()
                .HasForeignKey(p => new { p.Id, p.TypeId });
        }
    }
}