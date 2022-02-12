using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Utilizador;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.Perfis
{
    public interface IPerfilRepository : IRepository<Perfil, PerfilId>
    {
        Task<Perfil> GetByUserIdAsync(UserId id);
        Task<List<Perfil>>GetUsersWithSearchTagFieldAsync(UserId id,Tag tag);
    }
}