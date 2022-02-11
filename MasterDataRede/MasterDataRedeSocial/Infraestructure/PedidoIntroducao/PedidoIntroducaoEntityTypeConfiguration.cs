using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infrastructure.PedidoIntroducao
{
    internal class PedidoIntroducaoEntityTypeConfiguration : IEntityTypeConfiguration<Domain.PedidoIntroducao.PedidoIntroducao>
    {
        public void Configure(EntityTypeBuilder<Domain.PedidoIntroducao.PedidoIntroducao> builder)
        {
            // cf. https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx
            
            builder.HasKey(b => b.Id);
            builder.OwnsOne(b => b.estado);
            builder.OwnsOne(b => b.apresentacao);
            builder.OwnsOne(b => b.apresentacaoLigacao);
            builder.HasOne(b => b.destinatario);
            builder.HasOne(b => b.intermediario);
            builder.HasOne(b => b.remetente);

            //builder.Property<bool>("_active").HasColumnName("Active");
        }
    }
}