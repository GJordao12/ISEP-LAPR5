
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infrastructure.Ligacao
{
    internal class LigacaoEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Ligacoes.Ligacao>
    {
        public void Configure(EntityTypeBuilder<Domain.Ligacoes.Ligacao> builder)
        {
            // cf. https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx
            
            //builder.ToTable("Categories", SchemaNames.DDDSample1);
            builder.HasKey(b => b.Id);
            builder.OwnsOne(b => b.ForcaLigacao);
            builder.OwnsOne(b => b.ForçaRelacao);
            builder.HasMany(b => b.ListaTags);

            //builder.Property<bool>("_active").HasColumnName("Active");
        }
    }
}