using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.PedidoIntroducao
{
    public interface IPedidoIntroducaoRepository : IRepository<PedidoIntroducao, PedidoIntroducaoId>
    {
        Task<List<PedidoIntroducao>> GetByEstadoPendenteAsync(string userId);
    }
}