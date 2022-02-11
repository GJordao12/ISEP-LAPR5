using DDDSample1.Domain.EstadosDeHumor;
using DDDSample1.Infrastructure.Shared;

namespace DDDSample1.Infrastructure.EstadosDeHumor
{
    public class EstadoDeHumorRepository : BaseRepository<EstadoDeHumor, EstadoDeHumorId>, IEstadoDeHumorRepository

    {
        public EstadoDeHumorRepository(DDDSample1DbContext context) : base(context.EstadosDeHumor)
        {
        }
    }
}