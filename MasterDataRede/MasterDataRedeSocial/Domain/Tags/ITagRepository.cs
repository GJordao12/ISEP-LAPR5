using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Tags
{
    public interface ITagRepository : IRepository<Tag, TagID>
    {
        Task<List<Tag>> GetByNomeAsync(string nome);
    }
}