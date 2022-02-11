using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.Perfis;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infrastructure.Perfis
{
    internal class PerfilEntityTypeConfiguration : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.HasKey(b => b.Id);
            builder.OwnsOne(b => b.perfilDataDeNascimento);
            builder.OwnsOne(b => b.perfilFacebook);
            builder.OwnsOne(b => b.perfilLinkedin);
            builder.OwnsOne(b => b.perfilNome);
            builder.OwnsOne(b => b.perfilNTelefone);
        }
    }
}