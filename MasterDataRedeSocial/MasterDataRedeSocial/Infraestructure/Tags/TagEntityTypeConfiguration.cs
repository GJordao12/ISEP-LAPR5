using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infrastructure.Tags
{
    internal class TagEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Tags.Tag>
    {
        public void Configure(EntityTypeBuilder<Domain.Tags.Tag> builder)
        {
            // cf. https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx
            
            //builder.ToTable("Categories", SchemaNames.DDDSample1);
            builder.HasKey(b => b.Id);
            builder.OwnsOne(b => b.nome);
            //builder.Property<bool>("_active").HasColumnName("Active");
        }
    }
}