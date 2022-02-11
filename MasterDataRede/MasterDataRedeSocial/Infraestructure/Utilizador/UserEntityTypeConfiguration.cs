using DDDSample1.Domain.Utilizador;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infrastructure.Utilizador
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Utilizador.User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(b => b.Id);
            builder.OwnsOne(b => b.Email);
            builder.OwnsOne(b => b.Username);
            builder.OwnsOne(b => b.Password);
            
        }
    }
}