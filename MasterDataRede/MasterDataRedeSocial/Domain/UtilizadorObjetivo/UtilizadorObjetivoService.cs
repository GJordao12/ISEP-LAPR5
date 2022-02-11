
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Perfis;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.UtilizadorObjetivo
{
    public class UtilizadorObjetivoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repoUser;
        private readonly IPerfilRepository _repoPerfil;

        public UtilizadorObjetivoService(IUnitOfWork unitOfWork, IUserRepository repoUser,IPerfilRepository repoPerfil)
        {
            this._unitOfWork = unitOfWork;
            this._repoUser = repoUser;
            this._repoPerfil = repoPerfil;
        }

        public async Task<List<PerfilPrologDTO>> GetUtilizadoresObjetivo()
        {
            var listPerfis = await _repoPerfil.GetAllAsync();
            var listUsers = await _repoUser.GetAllAsync();
            var lisPerfilDto = new List<PerfilPrologDTO>();
            for (var i=0;i<listUsers.Count;i++)
            {
                for (var p = 0; p < listPerfis.Count; p++)
                {
                    if (listPerfis[p].UserId.Value.Equals(listUsers[i].Id.Value))
                    {
                        var listTag = new List<string>();
                        var user=new UserPrologDTO(new Guid(listUsers[i].Id.Value),listUsers[i].Username.userName);
                        List<Tag> tagList = listPerfis[p].ListaTags.ToList();
                        for (int k = 0; k < tagList.Count; k++)
                        {
                            listTag.Add(tagList[k].nome.nome);
                        }
                        var perfil = new PerfilPrologDTO(user, listTag,listPerfis[p].valorDesapontado,listPerfis[p].valorAlegria,listPerfis[p].valorAliviado,listPerfis[p].valorAngustiado,
                            listPerfis[p].valorEsperancoso,listPerfis[p].valorGrato,listPerfis[p].valorMedroso,listPerfis[p].valorRaivoso,listPerfis[p].valorOrgulhoso,listPerfis[p].valorComRemorsos);
                        lisPerfilDto.Add(perfil);
                    }
                }
            }
            return lisPerfilDto;
        }
    }
}