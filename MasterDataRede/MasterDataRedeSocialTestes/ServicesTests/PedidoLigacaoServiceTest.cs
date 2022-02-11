using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Ligacoes;
using DDDSample1.Domain.PedidoLigacao;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Utilizador;
using FluentAssertions;
using Moq;
using Xunit;

namespace MasterDataRedeSocialTestes
{
    public class PedidoLigacaoServiceTest
    {
        private readonly PedidoLigacaoService _service;
        private readonly Mock<IPedidoLigacaoRepository> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<ILigacaoRepository> _repositoryLigacaoMock;

        public PedidoLigacaoServiceTest()
        {
            _repositoryMock=new Mock<IPedidoLigacaoRepository>();
            _repositoryLigacaoMock = new Mock<ILigacaoRepository>();
            _service = new PedidoLigacaoService(_unitOfWorkMock.Object, _repositoryMock.Object,_repositoryLigacaoMock.Object);
        }

        [Fact]
        public void GetAllAsyncTest()
        {
            var list = new List<PedidoLigacao>();
            Estado estado = new Estado("Pendente");
            UserId remetente=new UserId(new Guid());
            UserId destinatario=new UserId(new Guid());
            Texto txt = new Texto("Olá, quero-me ligar a ti.");
            list.Add(new PedidoLigacao(remetente,destinatario, estado, txt));
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var result = _service.GetAllAsync();
            List<PedidoLigacaoDTO> resultDTO =
                list.ConvertAll<PedidoLigacaoDTO>(pedido => PedidoLigacaoDTOParser.ParaDTO(pedido));
            Assert.Equal(resultDTO.ToString(), result.Result.ToString());
        }
        
        [Fact]
        public void GetByIdTest()
        {
            Estado estado = new Estado("Pendente");
            UserId remetente=new UserId(new Guid());
            UserId destinatario=new UserId(new Guid());
            Texto txt = new Texto("Olá, quero-me ligar a ti.");

            PedidoLigacao pedidoLigacao = new PedidoLigacao(remetente, destinatario, estado, txt);

            _repositoryMock.Setup(x => x.GetByIdAsync(pedidoLigacao.Id)).ReturnsAsync(pedidoLigacao);
            var result =  _service.GetById(pedidoLigacao.Id);
            PedidoLigacaoDTO pedidoLigacaoDTO = PedidoLigacaoDTOParser.ParaDTO(pedidoLigacao);
            
            pedidoLigacaoDTO.Should().BeEquivalentTo(result.Result.Value);
        }
        
        [Fact]
        public void GetByIdTestNull()
        {
            _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<PedidoLigacaoId>())).ReturnsAsync(() => null);
            var result =  _service.GetById(new PedidoLigacaoId(new Guid()));
            Assert.Null(result.Result);
        }
        
        [Fact]
        public void GetByEstadoPendenteTest()
        {
            Estado estado = new Estado("Pendente");
            UserId remetente=new UserId(new Guid());
            UserId destinatario=new UserId(new Guid());
            Texto txt = new Texto("Olá, quero-me ligar a ti.");

            PedidoLigacao pedidoLigacao = new PedidoLigacao(remetente, destinatario, estado, txt);
            var list = new List<PedidoLigacao>();
            list.Add(pedidoLigacao);
            _repositoryMock.Setup(x => x.GetByEstadoPendenteAsync(estado.estado)).ReturnsAsync(list);
            var result =  _service.GetByEstadoPendente(estado.estado);
            var listDTO = list.ConvertAll<PedidoLigacaoDTO>(pedido => PedidoLigacaoDTOParser.ParaDTO(pedido));
            
            listDTO.Should().BeEquivalentTo(result.Result);
        }
        
        [Fact]
        public void GetByEstadoPendenteTestNull()
        {
            _repositoryMock.Setup(x => x.GetByEstadoPendenteAsync(It.IsAny<String>())).ReturnsAsync(() => null);
            var result =  _service.GetByEstadoPendente("7389f1d9-4d80-4bb3-9648-aa9fea779e8z");
            Assert.Null(result.Result);
        }
        
        [Fact]
        public void AddAsyncTest()
        {
            String texto = "Queria fazer uma ligação contigo";

            CreatingPedidoLigacaoDTO pedido = new CreatingPedidoLigacaoDTO(new UserId(Guid.NewGuid()).Value,new UserId(Guid.NewGuid()).Value, texto);

            var pedidoNovo = PedidoLigacaoDTOParser.DeDTOSemID(pedido);
            
            PedidoLigacaoDTO pedidoDTO = PedidoLigacaoDTOParser.ParaDTOSemPassUser(pedidoNovo);
            
            _repositoryMock.Setup(x => x.AddAsync(pedidoNovo)).ReturnsAsync(pedidoNovo);
            
            Task<PedidoLigacaoDTO> result = _service.AddAsync(pedido);

            pedidoDTO.ToString().Should().BeEquivalentTo(result.Result.ToString());
        }
        [Fact]
        public void AddAsyncTestNull()
        {
            var list = new List<PedidoLigacao>();

            String texto = "Queria fazer uma ligação contigo";
            
            CreatingPedidoLigacaoDTO pedido = new CreatingPedidoLigacaoDTO(new UserId(Guid.NewGuid()).Value,new UserId(Guid.NewGuid()).Value, texto);
            
            var pedidoNovo = PedidoLigacaoDTOParser.DeDTOSemID(pedido);
            
            list.Add(pedidoNovo);
            _repositoryMock.Setup(x => x.GetPedidoLigacaoByUsers(new UserId(pedido.remetente),new UserId(pedido.destinatario))).ReturnsAsync(list);
            _repositoryMock.Setup(x => x.AddAsync(pedidoNovo)).ReturnsAsync(pedidoNovo);
            
            Task<PedidoLigacaoDTO> result =_service.AddAsync(pedido);
            
            Assert.Null(result.Result);
        }
        [Fact]
        public void UpdateAsyncTest()
        {
            Estado estado = new Estado("Pendente");
            Texto apresentacao = new Texto("Olá, pretendia ligar-me a ti.");
            
            PedidoLigacao pedidoLigacao =
                new PedidoLigacao(new UserId(Guid.NewGuid()),new UserId(Guid.NewGuid()),estado, apresentacao);
            
            _repositoryMock.Setup(x => x.GetByIdAsync(new PedidoLigacaoId("7389f1d9-4d80-4bb3-9648-aa9fea779e8f")))
                .ReturnsAsync(pedidoLigacao);

            PedidoLigacaoDTOParser.ParaDTO(pedidoLigacao).Should()
                .BeEquivalentTo(_service.UpdatePedidoLigacaoAsync(new Guid("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"), estado).Result);
        }
        
        [Fact]
        public void UpdateAsyncTest2()
        {
            Estado estado = new Estado("Aceite");
            Texto apresentacao = new Texto("Olá, pretendia ligar-me a ti.");
            
            PedidoLigacao pedidoLigacao =
                new PedidoLigacao(new UserId(Guid.NewGuid()),new UserId(Guid.NewGuid()),estado, apresentacao);
            
            _repositoryMock.Setup(x => x.GetByIdAsync(new PedidoLigacaoId("7389f1d9-4d80-4bb3-9648-aa9fea779e8f")))
                .ReturnsAsync(pedidoLigacao);

            PedidoLigacaoDTOParser.ParaDTO(pedidoLigacao).Should()
                .BeEquivalentTo(_service.UpdatePedidoLigacaoAsync(new Guid("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"), estado).Result);
        }
        
        [Fact]
        public void UpdateAsyncTestNull()
        {
            Estado estado = new Estado("Aceite");
            Texto apresentacao = new Texto("Olá, pretendia ligar-me a ti.");
            
            PedidoLigacao pedidoLigacao =
                new PedidoLigacao(new UserId(Guid.NewGuid()),new UserId(Guid.NewGuid()),estado, apresentacao);
            
            _repositoryMock.Setup(x => x.GetByIdAsync(new PedidoLigacaoId("7389f1d9-4d80-4bb3-9648-aa9fea779e8f")))
                .ReturnsAsync(() => null);

            var result=_service.UpdatePedidoLigacaoAsync(new Guid("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"), estado);
            Assert.Null(result.Result);
        }
    }
}