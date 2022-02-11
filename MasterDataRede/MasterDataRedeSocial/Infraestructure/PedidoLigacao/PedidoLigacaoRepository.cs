using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.PedidoLigacao;
using DDDSample1.Domain.Utilizador;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure.PedidosLigacao
{
    public class PedidoLigacaoRepository : BaseRepository<Domain.PedidoLigacao.PedidoLigacao, PedidoLigacaoId>, IPedidoLigacaoRepository

    {
        public PedidoLigacaoRepository(DDDSample1DbContext context) : base(context.PedidoLigacao)
        {

        }

        public async Task<List<Domain.PedidoLigacao.PedidoLigacao>> GetByEstadoPendenteAsync(string userId)
        {
            return await _objs.Where(r => r.estado.estado.Equals("Pendente") && r.destinatario == new UserId(userId)).ToListAsync();
        }
        public async Task<List<Domain.PedidoLigacao.PedidoLigacao>> GetPedidoLigacaoByUsers(UserId remetente, UserId destinatario)
        {
            return await _objs.Where(r =>r.remetente.Equals(remetente) && r.destinatario.Equals(destinatario) && (r.estado.estado.Equals("Pendente")||r.estado.estado.Equals("Aceite"))).ToListAsync();
        }
    }
}