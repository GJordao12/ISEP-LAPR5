using System;
using System.Collections.Generic;
using DDDSample1.Domain.EstadosDeHumor;
using DDDSample1.Domain.Perfis;
using DDDSample1.Domain.Shared;
using Moq;
using Xunit;

namespace MasterDataRedeSocialTestes
{
    public class EstadoDeHumorServiceTest
    {
        private readonly EstadoDeHumorService _service;
        private readonly Mock<IEstadoDeHumorRepository> _repositoryMock = new Mock<IEstadoDeHumorRepository>();
        private readonly Mock<IPerfilRepository> _repositoryMockPerfil = new Mock<IPerfilRepository>();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        public EstadoDeHumorServiceTest()
        {
            _service = new EstadoDeHumorService(_unitOfWorkMock.Object, _repositoryMock.Object,_repositoryMockPerfil.Object);
        }
        
        [Fact]
        public void GetAllAsyncTest()
        {
            var list = new List<EstadoDeHumor>();
            list.Add(new EstadoDeHumor(new EstadoDeHumorId(new Guid()),"Feliz"));
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            List<EstadoDeHumorDto> resultDTO = list.ConvertAll<EstadoDeHumorDto>(estadoDeHumor => EstadoDeHumorDtoParser.ParaDTO(estadoDeHumor));
            var result =  _service.GetAllAsync();
            Assert.Equal(resultDTO.ToString(),result.Result.ToString());
        }
    }
}