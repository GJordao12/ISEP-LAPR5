using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.PedidoIntroducao;
using DDDSample1.Domain.Utilizador;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure.PedidoIntroducao
{
    public class PedidoIntroducaoRepository : BaseRepository<Domain.PedidoIntroducao.PedidoIntroducao, PedidoIntroducaoId>, IPedidoIntroducaoRepository
    {
        public PedidoIntroducaoRepository(DDDSample1DbContext context) : base(context.PedidoIntroducao)
        {
            
        }

        public async Task<List<Domain.PedidoIntroducao.PedidoIntroducao>> GetByEstadoPendenteAsync(string userId)
        {
            return await this._objs.Where(r => r.estado.estadoPedidoIntroducao.Equals("PENDENTE") && r.intermediario.Id == new UserId(userId)).ToListAsync();
        }
    }
}