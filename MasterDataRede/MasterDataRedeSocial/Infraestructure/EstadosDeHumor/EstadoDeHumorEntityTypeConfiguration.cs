using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDSample1.Domain.EstadosDeHumor;

namespace DDDSample1.Infrastructure.EstadosDeHumor
{
    internal class EstadoDeHumorEntityTypeConfiguration : IEntityTypeConfiguration<EstadoDeHumor>
    {
        public void Configure(EntityTypeBuilder<EstadoDeHumor> builder)
        {
            builder.HasKey(b => b.Id);
        }
    }
}