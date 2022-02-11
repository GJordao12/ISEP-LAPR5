
using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.Core.Internal;
using DDDSample1.Domain.Perfis;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.Pesquisa
{
    public class PesquisaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repoUser;
        private readonly IPerfilRepository _repoPerfil;
        private readonly ITagRepository _repoTag;

        public PesquisaService(IUnitOfWork unitOfWork, IUserRepository repoUser,IPerfilRepository repoPerfil,ITagRepository repoTag)
        {
            this._unitOfWork = unitOfWork;
            this._repoUser = repoUser;
            this._repoPerfil = repoPerfil;
            this._repoTag = repoTag;
        }

        public async Task<List<UserDTO>> GetUsersWithSearchField(UserId id,string nome,string email,string tag)
        {
            List<User> listUsers = new List<User>();
            List<User> listUsers2 = new List<User>();
            List<User> listUsers3 = new List<User>();
            List<User> listUsers4 = new List<User>();
            if (!email.Equals("null") && !tag.Equals("null") && !nome.Equals("null") )
            {
                listUsers2=await _repoUser.GetUsersWithSearchEmailFieldAsync(id, email);
                
                List<Tag> listTag = await _repoTag.GetByNomeAsync(tag);
                List<Perfil> listPerfis = await _repoPerfil.GetUsersWithSearchTagFieldAsync(id, listTag[0]);
                foreach (var perfil in listPerfis)
                {
                    listUsers3.Add( _repoUser.GetByIdAsync(perfil.UserId).Result);
                }
                listUsers4 = await _repoUser.GetUsersWithSearchNameFieldAsync(id, nome);

                if (!listUsers2.IsNullOrEmpty() && !listUsers3.IsNullOrEmpty()&& !listUsers4.IsNullOrEmpty())
                {
                    foreach (var x in listUsers2)
                    {
                        foreach (var y in listUsers3)
                        {
                            foreach (var z in listUsers4)
                            {
                                if (x.Id.Value.Equals(y.Id.Value)&&x.Id.Value.Equals(z.Id.Value))
                                {
                                    listUsers.Add(x);
                                }
                            }
                        }
                    }
                }
            }
            else if (!email.Equals("null") && !tag.Equals("null"))
            {
                listUsers2=await _repoUser.GetUsersWithSearchEmailFieldAsync(id, email);
                
                List<Tag> listTag = await _repoTag.GetByNomeAsync(tag);
                List<Perfil> listPerfis = await _repoPerfil.GetUsersWithSearchTagFieldAsync(id, listTag[0]);
                foreach (var perfil in listPerfis)
                {
                    listUsers3.Add( _repoUser.GetByIdAsync(perfil.UserId).Result);
                }

                if (!listUsers2.IsNullOrEmpty() && !listUsers3.IsNullOrEmpty())
                {
                    foreach (var x in listUsers2)
                    {
                        foreach (var y in listUsers3)
                        {
                            if (x.Id.Value.Equals(y.Id.Value))
                            {
                                listUsers.Add(x);
                            }
                        }
                    }
                }
            }
            else if (!email.Equals("null") && !nome.Equals("null"))
            {
                listUsers2=await _repoUser.GetUsersWithSearchEmailFieldAsync(id, email);
                listUsers3 = await _repoUser.GetUsersWithSearchNameFieldAsync(id, nome);

                if (!listUsers2.IsNullOrEmpty() && !listUsers3.IsNullOrEmpty())
                {
                    foreach (var x in listUsers2)
                    {
                        foreach (var y in listUsers3)
                        {
                            if (x.Id.Value.Equals(y.Id.Value))
                            {
                                listUsers.Add(x);
                            }
                        }
                    }
                }
            }
            else if (!tag.Equals("null") && !nome.Equals("null"))
            {
                listUsers2 = await _repoUser.GetUsersWithSearchNameFieldAsync(id, nome);
                List<Tag> listTag = await _repoTag.GetByNomeAsync(tag);
                List<Perfil> listPerfis = await _repoPerfil.GetUsersWithSearchTagFieldAsync(id, listTag[0]);
                foreach (var perfil in listPerfis)
                {
                    listUsers3.Add( _repoUser.GetByIdAsync(perfil.UserId).Result);
                }
                if (!listUsers2.IsNullOrEmpty() && !listUsers3.IsNullOrEmpty())
                {
                    foreach (var x in listUsers2)
                    {
                        foreach (var y in listUsers3)
                        {
                            if (x.Id.Value.Equals(y.Id.Value))
                            {
                                listUsers.Add(x);
                            }
                        }
                    }
                }
            }
            else if (!nome.Equals("null"))
            {
                listUsers = await _repoUser.GetUsersWithSearchNameFieldAsync(id, nome);
            }
            else if (!email.Equals("null"))
            {
                listUsers=await _repoUser.GetUsersWithSearchEmailFieldAsync(id, email);
            }
            else if (!tag.Equals("null"))
            {
                List<Tag> listTag = await _repoTag.GetByNomeAsync(tag);
                List<Perfil> listPerfis = await _repoPerfil.GetUsersWithSearchTagFieldAsync(id, listTag[0]);
                foreach (var perfil in listPerfis)
                {
                    listUsers.Add( _repoUser.GetByIdAsync(perfil.UserId).Result);
                }
            }
            List<UserDTO> list = new List<UserDTO>();
            if (!listUsers.IsNullOrEmpty())
            {
                foreach (var user in listUsers)
                {
                    list.Add(UserDTOParser.ParaDTO(user));
                }
            }

            return list;
        }
    }
}