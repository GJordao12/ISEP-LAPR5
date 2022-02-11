
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using DDDSample1.Domain.Ligacoes;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Utilizador;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Domain.Tags
{
    public class TagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITagRepository _repo;
        private readonly IUserRepository _repoU;
        private readonly ILigacaoRepository _repoC;

        public TagService(IUnitOfWork unitOfWork, ITagRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<List<TagDTO>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            List<TagDTO> listDto = list.ConvertAll<TagDTO>(ligacao => TagDTOParser.ParaDTO(ligacao));

            return listDto;
        }

        public async Task<ActionResult<IEnumerable<TagDTO>>> GetTagsAllUsersAsync()
        {
            var listAllTags = await _repo.GetAllAsync();
            var listTags = new List<Tag>();

            foreach (Tag tag in listAllTags)
            {
                if (tag.perfis.Count > 0)
                {
                    listTags.Add(tag);
                }
            }

            List<TagDTO> listDto = listTags.ConvertAll<TagDTO>(tag => TagDTOParser.ParaDTO(tag));

            return listDto;
        }
        
        public async Task<ActionResult<Dictionary<string,int>>> GetTagsCloudAllUsersAsync()
        {
            var listAllTags = await _repo.GetAllAsync();
            var mapTags = new Dictionary<string,int>();

            foreach (Tag tag in listAllTags)
            {
                if (tag.perfis.Count > 0)
                {
                    mapTags.Add(tag.nome.nome,tag.perfis.Count);
                }
            }

            return mapTags.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);;
        }
        
        
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetTagsByUserAsync(string id)
        {
            var listAllTags = await _repo.GetAllAsync();
            var listTags = new List<Tag>();

            foreach (Tag tag in listAllTags)
            {
                foreach (var perfil in tag.perfis)
                {
                    if (perfil.UserId.Value.Equals(id) && !listTags.Contains(tag))
                    {
                        listTags.Add(tag);
                    }
                }
            }
            
            List<TagDTO> listDto = listTags.ConvertAll<TagDTO>(tag => TagDTOParser.ParaDTO(tag));

            return listDto;
        }
        
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetTagsConnectionsAsync()
        {
            var listAllTags = await _repo.GetAllAsync();
            var listTagsConnection = new List<Tag>();

            foreach (Tag tag in listAllTags)
            {
                if (tag.ligacoes.Count > 0)
                {
                    listTagsConnection.Add(tag);
                }
            }

            List<TagDTO> listDto = listTagsConnection.ConvertAll<TagDTO>(tag => TagDTOParser.ParaDTO(tag));

            return listDto;
        }
        
        public async Task<ActionResult<Dictionary<string,int>>> GetTagsCloudConnectionsAsync()
        {
            var listAllTags = await _repo.GetAllAsync();
            var mapTags = new Dictionary<string,int>();

            foreach (Tag tag in listAllTags)
            {
                if (tag.ligacoes.Count > 0)
                {
                    mapTags.Add(tag.nome.nome,tag.ligacoes.Count);
                }
            }

            return mapTags.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }
        
        
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetTagsConnectionsByUserAsync(string id)
        {
            var listAllTags = await _repo.GetAllAsync();
            var listTags = new List<Tag>();

            foreach (Tag tag in listAllTags)
            {
                foreach (var ligacao in tag.ligacoes)
                {
                    if ((ligacao.Principal.Value.Equals(id) || ligacao.Secundario.Value.Equals(id)) && !listTags.Contains(tag))
                    {
                        listTags.Add(tag);
                    }
                }
            }
            
            List<TagDTO> listDto = listTags.ConvertAll<TagDTO>(tag => TagDTOParser.ParaDTO(tag));

            return listDto;
        }

        public async Task<Tag> AddTags(string name)
        {
            var tag = await _repo.GetByNomeAsync(name);

            if (tag.IsNullOrEmpty())
            {
                Tag novaTag = new Tag(new Nome(name));
                await _repo.AddAsync(novaTag);
                await _unitOfWork.CommitAsync();
                return novaTag;
            }

            return tag.First();
        }

        public async Task<TagDTO> GetTagsByIdAsync(string id)
        {
            
            var tag = _repo.GetByIdAsync(new TagID(id)).Result;
            if (tag != null)
            {
                return TagDTOParser.ParaDTO(tag);
            }

            return null;
        }
    }
}