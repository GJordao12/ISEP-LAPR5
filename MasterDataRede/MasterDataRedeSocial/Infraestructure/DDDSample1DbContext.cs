using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.EstadosDeHumor;
using DDDSample1.Domain.Utilizador;
using DDDSample1.Domain.Perfis;
using DDDSample1.Infrastructure.EstadosDeHumor;
using DDDSample1.Infrastructure.Ligacao;
using DDDSample1.Infrastructure.PedidoIntroducao;
using DDDSample1.Infrastructure.PedidoLigacao;
using DDDSample1.Infrastructure.Perfis;
using DDDSample1.Infrastructure.Tags;
using DDDSample1.Infrastructure.Utilizador;


namespace DDDSample1.Infrastructure
{
    public class DDDSample1DbContext : DbContext
    {
        public DbSet<EstadoDeHumor> EstadosDeHumor { get; set; }
        
        public DbSet<Domain.PedidoIntroducao.PedidoIntroducao> PedidoIntroducao { get; set; }
        
        public DbSet<Domain.PedidoLigacao.PedidoLigacao> PedidoLigacao { get; set; }
        
        public DbSet<Domain.Ligacoes.Ligacao> Ligacao { get; set; }
        
        public DbSet<Domain.Tags.Tag> Tag { get; set; }
        
        public DbSet<User> User { get; set; }
        
        public DbSet<Perfil> Perfil { get; set; }

        public DDDSample1DbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Ligacoes.Ligacao>()
                .HasMany<Domain.Tags.Tag>(s => s.ListaTags)
                .WithMany(c => c.ligacoes);
            
            modelBuilder.Entity<Perfil>()
                .HasMany<Domain.Tags.Tag>(s => s.ListaTags)
                .WithMany(c => c.perfis);
            
            modelBuilder.ApplyConfiguration(new EstadoDeHumorEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoIntroducaoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoLigacaoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TagEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LigacaoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PerfilEntityTypeConfiguration());
        }
    }
}