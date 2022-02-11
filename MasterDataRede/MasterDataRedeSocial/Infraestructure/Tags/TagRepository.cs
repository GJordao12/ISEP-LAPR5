
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Tags;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure.Tags
{
    public class TagRepository : BaseRepository<Tag,TagID>, ITagRepository

    {
        public TagRepository(DDDSample1DbContext context) : base(context.Tag)
        {
            
        }
        public async Task<List<Tag>> GetByNomeAsync(string nome)
        {
            return await this._objs.Where(r => r.nome.nome.Equals(nome)).ToListAsync();
        }
    }
}