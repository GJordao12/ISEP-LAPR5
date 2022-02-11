using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Ligacoes;
using DDDSample1.Domain.Perfis;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.RedesConexoes
{
    public class RedeDeConexoesService
    {
        private readonly ILigacaoRepository _repo;
        private readonly IUserRepository _repoUser;
        private readonly IPerfilRepository _repoPerfil;

        public RedeDeConexoesService(ILigacaoRepository repo, IUserRepository repoUser,
            IPerfilRepository repoPerfil)
        {
            this._repo = repo;
            this._repoUser = repoUser;
            this._repoPerfil = repoPerfil;
        }

        public async Task<Dictionary<int, List<LigacaoRedeDeConexoesDTO>>> GetByIdAndLevelAsync(UserId id, int niveis)
        {
            var user = await _repoUser.GetByIdAsync(id);

            if (user == null)
            {
                throw new BusinessRuleValidationException("[ERROR] Utilizador ID Inválido");
            }

            var dic = await this._repo.GetByIdAndLevelAsync(id, niveis);

            Dictionary<int, List<LigacaoRedeDeConexoesDTO>> dicDTO =
                new Dictionary<int, List<LigacaoRedeDeConexoesDTO>>();

            foreach (var nivel in dic.Keys)
            {
                List<LigacaoRedeDeConexoesDTO> listDTO = new List<LigacaoRedeDeConexoesDTO>();
                foreach (var ligacao in dic[nivel])
                {
                    User userSecundario = await _repoUser.GetByIdAsync(ligacao.Secundario);
                    Perfil perfilUserSecundario = await _repoPerfil.GetByUserIdAsync(userSecundario.Id);
                    List<TagDTO> listaTagSecundarioDto = new List<TagDTO>();
                    List<TagDTO> listaTagLigacaoDto = new List<TagDTO>();

                    foreach (var tag in perfilUserSecundario.ListaTags)
                    {
                        TagDTO tagDto = TagDTOParser.ParaDTO(tag);
                        listaTagSecundarioDto.Add(tagDto);
                    }

                    foreach (var tag in ligacao.ListaTags)
                    {
                        TagDTO tagDto = TagDTOParser.ParaDTO(tag);
                        listaTagLigacaoDto.Add(tagDto);
                    }

                    listDTO.Add(new LigacaoRedeDeConexoesDTO(ligacao.Id, ligacao.Principal,
                        UserDTOParser.ParaDTOSemPass(userSecundario), listaTagSecundarioDto,
                        ligacao.ForcaLigacao.forcaLigacao, ligacao.ForçaRelacao.forcaRelacao, listaTagLigacaoDto));
                }

                dicDTO.Add(nivel, listDTO);
            }
            return dicDTO;
        }
    }
}