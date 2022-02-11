using System;
using DDDSample1.Domain.EstadosDeHumor;
using DDDSample1.Domain.Ligacoes;
using DDDSample1.Domain.PedidoIntroducao;
using DDDSample1.Domain.PedidoLigacao;
using DDDSample1.Domain.Perfis;
using DDDSample1.Domain.Pesquisa;
using DDDSample1.Domain.RedesConexoes;
//using DDDSample1.Domain.PedidoIntroducao;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.EstadosDeHumor;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;
using DDDSample1.Domain.UtilizadorObjetivo;
using DDDSample1.Infrastructure.Ligacao;
using DDDSample1.Infrastructure.PedidoIntroducao;
using DDDSample1.Infrastructure.PedidosLigacao;
using DDDSample1.Infrastructure.Perfis;
using DDDSample1.Infrastructure.Shared;
using DDDSample1.Infrastructure.Tags;
using DDDSample1.Infrastructure.Utilizador;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDDSample1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DDDSample1DbContext>(opt =>
                opt.UseMySql(
                        connectionString: @"server=;user=root;password=;database=",
                        new MySqlServerVersion(new Version(10, 4, 17)))
                    .ReplaceService<IValueConverterSelector, StronglyEntityIdValueConverterSelector>()
                    .UseLazyLoadingProxies());
            ConfigureMyServices(services);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureMyServices(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork,UnitOfWork>();

            services.AddTransient<IEstadoDeHumorRepository,EstadoDeHumorRepository>();
            services.AddTransient<EstadoDeHumorService>();

            services.AddTransient<IPedidoIntroducaoRepository, PedidoIntroducaoRepository>();
            services.AddTransient<PedidoIntroducaoService>();
           
            services.AddTransient<IPedidoLigacaoRepository, PedidoLigacaoRepository>();
            services.AddTransient<PedidoLigacaoService>();
            
            services.AddTransient<ILigacaoRepository, LigacaoRepository>();
            services.AddTransient<LigacaoService>();
           
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<TagService>();

            services.AddTransient<IUserRepository,UserRepository>();
            services.AddTransient<UserService>();
            
            services.AddTransient<IPerfilRepository,PerfilRepository>();
            services.AddTransient<PerfilService>();
            
            services.AddTransient<RedeDeConexoesService>();

            services.AddTransient<PesquisaService>();
            
            services.AddTransient<UtilizadorObjetivoService>();
        }
    }
}
