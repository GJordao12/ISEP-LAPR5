using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DDDSample1.Domain.Perfis;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.EstadosDeHumor
{
    public class EstadoDeHumorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEstadoDeHumorRepository _repo;
        private readonly IPerfilRepository _repoPerfil;
        public EstadoDeHumorService(IUnitOfWork unitOfWork, IEstadoDeHumorRepository repo, IPerfilRepository _repoPerfil )
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._repoPerfil = _repoPerfil;
        }

        public async Task<List<EstadoDeHumorDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            List<EstadoDeHumorDto> listDto = list.ConvertAll<EstadoDeHumorDto>(estadoHum =>
                EstadoDeHumorDtoParser.ParaDTO(estadoHum));

            return listDto;
        }

        public async Task<EstadoDeHumorDto> GetById(EstadoDeHumorId id)
        {
            var estado = await _repo.GetByIdAsync(id);

            if (estado == null)
            {
                return null;
            }

            return EstadoDeHumorDtoParser.ParaDTO(estado);
        }
        
        public async Task<EstadoDeHumorDto> GetEstadoHumorByUserId(UserId userId)
        {
            var perfil = await _repoPerfil.GetByUserIdAsync(userId);
            if (perfil == null)
            {
                throw new BusinessRuleValidationException(
                    "[ERROR] Não foi encontrado um perfil para o User ID pretendido.");
            }

            var estadoHumor = await _repo.GetByIdAsync(perfil.estadoDeHumorId);
            if (estadoHumor == null)
            {
                throw new BusinessRuleValidationException(
                    "[ERROR] Não foi encontrado nenhum estado de humor com o ID pretendido.");
            }

            return EstadoDeHumorDtoParser.ParaDTO(estadoHumor);
        }
    }
}