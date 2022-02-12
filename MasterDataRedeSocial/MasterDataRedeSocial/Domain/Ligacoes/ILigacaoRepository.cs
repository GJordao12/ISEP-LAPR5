using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.Ligacoes
{
    public interface ILigacaoRepository : IRepository<Ligacao, LigacaoID>
    {
        Task<List<Ligacao>> GetLigacaoByUsers(UserId remetente, UserId destinatario);

        Task<List<Ligacao>> GetLigacoesbyUser(UserId userId);

        Task<Dictionary<int, List<Ligacao>>> GetByIdAndLevelAsync(UserId id, int niveis);
    }
}