using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.PedidoLigacao
{
    public interface IPedidoLigacaoRepository : IRepository<PedidoLigacao, PedidoLigacaoId>
    {
        Task<List<PedidoLigacao>> GetByEstadoPendenteAsync(string userId);

        Task<List<PedidoLigacao>> GetPedidoLigacaoByUsers(UserId remetente, UserId destinatario);
    }
}