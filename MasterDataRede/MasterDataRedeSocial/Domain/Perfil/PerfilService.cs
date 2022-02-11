using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.Core.Internal;
using DDDSample1.Domain.EstadosDeHumor;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;
using Microsoft.AspNetCore.Mvc;


namespace DDDSample1.Domain.Perfis
{
    public class PerfilService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPerfilRepository _repo;
        private readonly IEstadoDeHumorRepository _repoEstadoDeHumor;
        private readonly IUserRepository _repoUser;
        private readonly ITagRepository _repoTags;

        public PerfilService(IUnitOfWork unitOfWork, IPerfilRepository repo,
            IEstadoDeHumorRepository _repoEstadoDeHumor, IUserRepository _repoUser, ITagRepository _repoTags)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._repoEstadoDeHumor = _repoEstadoDeHumor;
            this._repoUser = _repoUser;
            this._repoTags = _repoTags;
        }

        public async Task<PerfilDto> UpdateEstadoDeHumorAsync(Guid perfilId, EstadoDeHumorDto estadoDeHumorDto)
        {
            EstadoDeHumorId estadoDeHumorId = new EstadoDeHumorId(estadoDeHumorDto.Id);

            await checkEstadoDeHumorIdAndDescriptionAsync(estadoDeHumorId, estadoDeHumorDto.Description);

            var perfil = await this._repo.GetByIdAsync(new PerfilId(perfilId));

            if (perfil == null)
            {
                throw new BusinessRuleValidationException("[ERROR] Perfil ID Inválido");
            }

            //muda o campo do estado de humor id
            perfil.ChangeEstadoDeHumorId(estadoDeHumorId);

            await this._unitOfWork.CommitAsync();

            return PerfilDtoParser.ParaDTO(perfil);
        }

        public async Task<PerfilDto> UpdateEstadoDeHumorAsync2(UserId userId, EstadoDeHumorDto estadoDeHumorDto)
        {
            EstadoDeHumorId estadoDeHumorId = new EstadoDeHumorId(estadoDeHumorDto.Id);

            await checkEstadoDeHumorIdAndDescriptionAsync(estadoDeHumorId, estadoDeHumorDto.Description);

            var user = await _repoUser.GetByIdAsync(userId);

            if (user == null)
            {
                throw new BusinessRuleValidationException("[ERROR] User ID Inválido");
            }

            var perfil = await _repo.GetByUserIdAsync(userId);

            if (perfil == null)
            {
                throw new BusinessRuleValidationException(
                    "[ERROR] Não foi encontrado um perfil para o User ID pretendido");
            }

            //muda o campo do estado de humor id
            perfil.ChangeEstadoDeHumorId(estadoDeHumorId);

            await this._unitOfWork.CommitAsync();

            return PerfilDtoParser.ParaDTO(perfil);
        }

        private async Task checkEstadoDeHumorIdAndDescriptionAsync(EstadoDeHumorId estadoDeHumorId, string description)
        {
            var estadoDeHumor = await _repoEstadoDeHumor.GetByIdAsync(estadoDeHumorId);
            if (estadoDeHumor == null)
                throw new BusinessRuleValidationException("[ERROR] Estado De Humor ID Inválido");
            if (estadoDeHumor.Description != description)
                throw new BusinessRuleValidationException(
                    "[ERROR] O Estado De Humor ID recebido não corresponde à Descrição recebida");
        }

        public async Task<ActionResult<IEnumerable<PerfilDto>>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            List<PerfilDto> listDto = list.ConvertAll<PerfilDto>(perfil => PerfilDtoParser.ParaDTO(perfil));

            return listDto;
        }

        public async Task<ActionResult<PerfilDto>> GetByUserId(UserId userId)
        {
            var user = await _repoUser.GetByIdAsync(userId);

            if (user == null)
            {
                throw new BusinessRuleValidationException("[ERROR] User ID Inválido");
            }

            var perfil = await _repo.GetByUserIdAsync(userId);

            if (perfil == null)
            {
                throw new BusinessRuleValidationException(
                    "[ERROR] Não foi encontrado um perfil para o User ID pretendido");
            }

            return PerfilDtoParser.ParaDTO(perfil);
        }

        public async Task<ActionResult<PerfilDto>> GetById(PerfilId perfilId)
        {
            var perfil = await _repo.GetByIdAsync(perfilId);

            if (perfil == null)
            {
                return null;
            }

            return PerfilDtoParser.ParaDTO(perfil);
        }

        public async Task<PerfilDto> UpdateAsync(Guid id, CreatingPerfilPutDto perfilPutDto)
        {
            var perfil = await this._repo.GetByIdAsync(new PerfilId(id));

            if (perfil == null)
            {
                return null;
            }

            var estadoHumor =
                await this._repoEstadoDeHumor.GetByIdAsync(new EstadoDeHumorId(perfilPutDto.estadoDeHumor.Id));

            if (estadoHumor == null)
            {
                return null;
            }

            ICollection<Tag> listaTag = new List<Tag>();

            foreach (var tagDTO in perfilPutDto.listaTags)
            {
                var nome = tagDTO.nome.ToLower();
                var list = _repoTags.GetByNomeAsync(nome);

                if (list.Result.IsNullOrEmpty())
                {
                    var tag = new Tag(new TagID(Guid.NewGuid()), new Nome(nome));
                    await _repoTags.AddAsync(tag);
                    await _unitOfWork.CommitAsync();
                    listaTag.Add(tag);
                }
                else
                {
                    listaTag.Add(list.Result[0]);
                }
            }

            perfil.ChangeEstadoDeHumorId(new EstadoDeHumorId(perfilPutDto.estadoDeHumor.Id));
            perfil.UpdateListaTags(listaTag);
            perfil.UpdateDados(perfilPutDto.perfilDataDeNascimento, perfilPutDto.perfilFacebook,
                perfilPutDto.perfilLinkedin, perfilPutDto.perfilNome, perfilPutDto.perfilNTelefone);

            await this._unitOfWork.CommitAsync();

            return PerfilDtoParser.ParaDTO(perfil);
        }
    }
}