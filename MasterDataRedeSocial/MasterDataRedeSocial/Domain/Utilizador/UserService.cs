using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Perfis;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Utilizador
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repo;
        private readonly IPerfilRepository _repoPerfil;

        public UserService(IUnitOfWork unitOfWork, IUserRepository repo, IPerfilRepository repoPerfil)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._repoPerfil = repoPerfil;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            List<UserDTO> listDto = list.ConvertAll<UserDTO>(user => UserDTOParser.ParaDTO(user));

            return listDto;
        }

        public async Task<UserDTO> GetById(UserId id)
        {
            var user = await _repo.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            return UserDTOParser.ParaDTO(user);
        }

        public async Task<UserDTO> AddAsync(CreatingUserDTO dto)
        {
            var user = UserDTOParser.DeDTOSemID(dto);
            await this._repo.AddAsync(user);

            var perfil = new Perfil(Guid.NewGuid(), user.Id);
            await this._repoPerfil.AddAsync(perfil);

            await this._unitOfWork.CommitAsync();

            return UserDTOParser.ParaDTO(user);
        }

        public async Task<UserDTO> GetByName(string name)
        {
            var user = await _repo.GetUserByUsername(name);

            if (user == null)
            {
                return null;
            }

            return UserDTOParser.ParaDTO(user);
        }

        public async Task<UserDTO> DeleteAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);

            if (user == null)
                return null;

            this._repo.Remove(user);
            await this._unitOfWork.CommitAsync();

            return new UserDTO(user.Id.AsGuid(), user.Email.ToString(), user.Username.ToString());
        }
    }
}