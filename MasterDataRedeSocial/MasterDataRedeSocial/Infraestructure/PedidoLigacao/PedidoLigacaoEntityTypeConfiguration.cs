using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infrastructure.PedidoLigacao
{
    internal class PedidoLigacaoEntityTypeConfiguration : IEntityTypeConfiguration<Domain.PedidoLigacao.PedidoLigacao>
    {
        public void Configure(EntityTypeBuilder<Domain.PedidoLigacao.PedidoLigacao> builder)
        {
            // cf. https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx
            
            
            builder.HasKey(b => b.Id);
            builder.OwnsOne(b => b.estado);
            builder.OwnsOne(b => b.texto);
            //builder.Property<bool>("_active").HasColumnName("Active");
        }
    }
}