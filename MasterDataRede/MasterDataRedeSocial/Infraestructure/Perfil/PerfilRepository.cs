using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using DDDSample1.Domain.Perfis;
using DDDSample1.Domain.Utilizador;
using DDDSample1.Domain.Tags;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure.Perfis
{
    public class PerfilRepository : BaseRepository<Perfil, PerfilId>, IPerfilRepository
    {
        public PerfilRepository(DDDSample1DbContext context) : base(context.Perfil)
        {
            
        }
        public async Task<List<Perfil>> GetUsersWithSearchTagFieldAsync(UserId id,Tag tag)
        {
            return await _objs.Where(u=>!u.Id.Equals(id) && u.ListaTags.Contains(tag)).ToListAsync();
        }

        public async Task<Perfil> GetByUserIdAsync(UserId id)
        {
            return await _objs.Where(p => p.UserId.Equals(id)).FirstOrDefaultAsync();
        }
    }
}