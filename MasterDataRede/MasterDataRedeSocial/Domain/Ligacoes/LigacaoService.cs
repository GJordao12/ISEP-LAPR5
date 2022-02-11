using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Domain.Ligacoes
{
    public class LigacaoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILigacaoRepository _repo;
        private readonly ITagRepository _repoTags;
        private readonly IUserRepository _repoUsers;

        public LigacaoService(IUnitOfWork unitOfWork, ILigacaoRepository repo, ITagRepository repoTags, IUserRepository repoUsers)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._repoTags = repoTags;
            this._repoUsers = repoUsers;
        }

        public async Task<List<LigacaoDTO>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            List<LigacaoDTO> listDto = list.ConvertAll<LigacaoDTO>(ligacao => LigacaoDTOParser.ParaDTO(ligacao));

            return listDto;
        }

        public async Task<LigacaoDTO> AddAsync(CreatingLigacaoDTO dto)
        {
            var ligacao = LigacaoDTOParser.DeDTOSemID(dto);

            await this._repo.AddAsync(ligacao);

            await this._unitOfWork.CommitAsync();

            return LigacaoDTOParser.ParaDTO(ligacao);
        }

        public async Task<List<LigacaoDTO>> GetLigacoesByUser(UserId userId)
        {
            List<LigacaoDTO> ligacaoDtos = new List<LigacaoDTO>();

            foreach (var ligacao in this._repo.GetLigacoesbyUser(userId).Result)
            {
                LigacaoDTO ligacaoDto = LigacaoDTOParser.ParaDTO(ligacao);
                ligacaoDtos.Add(ligacaoDto);
            }

            return ligacaoDtos;
        }

        public async Task<ActionResult<LigacaoDTO>> GetById(LigacaoID ligacaoId)
        {
            var ligacao = await this._repo.GetByIdAsync(ligacaoId);

            if (ligacao == null)
            {
                return null;
            }

            return LigacaoDTOParser.ParaDTO(ligacao);
        }

        public async Task<LigacaoDTO> UpdateAsync(Guid id, LigacaoPutDTO dto)
        {
            var ligacao = await this._repo.GetByIdAsync(new LigacaoID(id));

            if (ligacao == null)
                return null;

            ICollection<Tag> listaTag = new List<Tag>();

            foreach (var tagDTO in dto.listaTags)
            {
                var nome = tagDTO.nome.ToLower();
                var list = _repoTags.GetByNomeAsync(nome).Result;

                if (list.Count == 0)
                {
                    var tag = new Tag(new TagID(Guid.NewGuid()), new Nome(nome));
                    await _repoTags.AddAsync(tag);
                    await _unitOfWork.CommitAsync();
                    listaTag.Add(tag);
                }
                else
                {
                    listaTag.Add(list[0]);
                }
            }

            ligacao.UpdateForcaLigacao(dto.forcaLigacao);
            ligacao.UpdateListaTags(listaTag);

            await this._unitOfWork.CommitAsync();

            return LigacaoDTOParser.ParaDTO(ligacao);
        }

        public async Task<List<LigacaoDTOProlog>> GetAllProlog()
        {
            var list = await _repo.GetAllAsync();

            List<LigacaoDTOProlog> ligacaoProlog = new List<LigacaoDTOProlog>();
            
            foreach (var ligacao in list)
            {
                foreach (var ligacaoS in list)
                {
                    if (ligacao.Principal.Equals(ligacaoS.Secundario) && ligacao.Secundario.Equals(ligacaoS.Principal))
                    {
                        LigacaoDTOProlog prologLigacao = new LigacaoDTOProlog(ligacao.Principal.Value, ligacao.Secundario.Value,
                            ligacao.ForcaLigacao.forcaLigacao, ligacaoS.ForcaLigacao.forcaLigacao,ligacao.ForçaRelacao.forcaRelacao);

                        if (!ligacaoProlog.Contains(prologLigacao))
                        {
                            ligacaoProlog.Add(prologLigacao);
                        }
                    }
                }
            }
            
            return ligacaoProlog;
        }

        public async Task<List<LeaderBoardDTO>> GetLeaderBoardFortaleza(string id)
        {
            var listaLigacoes = await this._repo.GetAllAsync();
            
            var auxMap = new Dictionary<string, int>();
            
                foreach (var ligacao in listaLigacoes)
                {
                    var username = _repoUsers.GetByIdAsync(ligacao.Principal).Result.Username.ToString();
                    Console.Write(username);
                
                    if (auxMap.ContainsKey(username))
                    {
                        auxMap[username] += ligacao.ForcaLigacao.forcaLigacao;
                    }
                    else
                    {
                        auxMap.Add(username,ligacao.ForcaLigacao.forcaLigacao);
                    }
                }
            
                var auxList = auxMap.ToList();
                auxList.Sort((par1,par2) => par2.Value.CompareTo(par1.Value));

                var newList = auxList.Take(10);
                
                var top10 = new List<LeaderBoardDTO>();

                var posicao = 1;
                foreach (var row in newList)
                {
                    top10.Add(new LeaderBoardDTO(row.Key,row.Value,posicao));
                    posicao++;
                }
                
                if (id != "-1")
                {
                    var usernameNossoUtilizador = _repoUsers.GetByIdAsync(new UserId(id)).Result.Username.ToString();

                    var posicaoUser = auxList.IndexOf(new KeyValuePair<string, int>(usernameNossoUtilizador,
                        auxMap[usernameNossoUtilizador]))+1;
                    
                    if (!top10.Any(x=> x.username == usernameNossoUtilizador))
                    {
                        top10.Add(new LeaderBoardDTO(usernameNossoUtilizador,auxMap[usernameNossoUtilizador],posicaoUser));
                    } 
                }
                
            return top10;
        }

        public async Task<List<LeaderBoardDTO>> GetLeaderBoardDimensao(string id)
        {
            var listUtilizadores = await _repoUsers.GetAllAsync();
            
            var auxMap = new Dictionary<string, int>();
            
            foreach (var user in listUtilizadores)
            {
                var total = 0;
                var listLigacoesUser = await _repo.GetLigacoesbyUser(user.Id);
                var username = _repoUsers.GetByIdAsync(user.Id).Result.Username.ToString();
                
                total += listLigacoesUser.Count;
                
                foreach (var ligacao in listLigacoesUser)
                {
                    var listLigacoesAmigo = await _repo.GetLigacoesbyUser(ligacao.Secundario);
                    total += listLigacoesAmigo.Count;
                }
                
                auxMap.Add(username,total);
            }
            
            var auxList = auxMap.ToList();
            auxList.Sort((par1, par2) => par2.Value.CompareTo(par1.Value));

            var newList = auxList.Take(10);
            var top10 = new List<LeaderBoardDTO>();
            
            var posicao = 1;
            foreach (var top in newList)
            {
                top10.Add(new LeaderBoardDTO(top.Key,top.Value,posicao));
                posicao++;
            }

            if (id != "-1")
            {
                var usernameNossoUtilizador = _repoUsers.GetByIdAsync(new UserId(id)).Result.Username.ToString();
                var posicaoUser = auxList.IndexOf(new KeyValuePair<string, int>(usernameNossoUtilizador,
                    auxMap[usernameNossoUtilizador]))+1;
                    
                if (!top10.Any(x=> x.username == usernameNossoUtilizador))
                {
                    top10.Add(new LeaderBoardDTO(usernameNossoUtilizador,auxMap[usernameNossoUtilizador],posicaoUser));
                }
            }

            return top10;
        }
        public async Task<LigacaoDTO> UpdateForcaRelacaoAsync(Guid idRemetente,Guid idDestinatario, string status)
        {
            var ligacao = await this._repo.GetLigacaoByUsers(new UserId(idRemetente),new UserId(idDestinatario));
            var ligacao2 = await this._repo.GetLigacaoByUsers(new UserId(idDestinatario),new UserId(idRemetente));
            if (ligacao == null)
                return null;
            var forca=0;
            if (status.Equals("like"))
            {
                forca = ligacao[0].ForçaRelacao.forcaRelacao + 1;
            }else if (status.Equals("dislike"))
            { 
                forca = ligacao[0].ForçaRelacao.forcaRelacao -1;
            }
            ligacao[0].UpdateForcaRelacao(new ForçaRelacao(forca));
            ligacao2[0].UpdateForcaRelacao(new ForçaRelacao(forca));


            await this._unitOfWork.CommitAsync();

            return LigacaoDTOParser.ParaDTO(ligacao[0]);
        }
        
    }
}