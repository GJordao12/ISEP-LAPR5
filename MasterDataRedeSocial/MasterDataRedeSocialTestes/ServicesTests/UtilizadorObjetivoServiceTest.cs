
using System.Collections.Generic;
using DDDSample1.Domain.Perfis;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Utilizador;
using DDDSample1.Domain.UtilizadorObjetivo;
using Moq;
using Xunit;

namespace MasterDataRedeSocialTestes
{
    public class UtilizadorObjetivoServiceTest
    {
        private readonly UtilizadorObjetivoService _service;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IUserRepository> _repoUserMock;
        private readonly Mock<IPerfilRepository> _repoPerfilMock;

        public UtilizadorObjetivoServiceTest()
        {
            _repoUserMock = new Mock<IUserRepository>();
            _repoPerfilMock = new Mock<IPerfilRepository>();
            _service = new UtilizadorObjetivoService(_unitOfWorkMock.Object, _repoUserMock.Object,_repoPerfilMock.Object);

        }
    }
}