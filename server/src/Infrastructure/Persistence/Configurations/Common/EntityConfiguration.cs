using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Common;

namespace Infrastructure.Persistence.Configurations
{
    public class EntityConfiguration : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            builder.HasKey(k => new { k.Id, k.TypeId }).HasName("pk_cd_entities");

            builder
                .HasOne<EntityType>(p => p.Type)
                .WithMany()
                .HasForeignKey(p => p.TypeId)
                .HasConstraintName("fk_cd_entities_cs_entity_types_f_type");
        }
    }
}