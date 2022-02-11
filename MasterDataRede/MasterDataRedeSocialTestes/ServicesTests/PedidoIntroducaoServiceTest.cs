using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Ligacoes;
using DDDSample1.Domain.PedidoIntroducao;
using DDDSample1.Domain.PedidoLigacao;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;
using FluentAssertions;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace MasterDataRedeSocialTestes
{
    public class PedidoIntroducaoServiceTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly PedidoIntroducaoService _service;
        private readonly Mock<IPedidoIntroducaoRepository> _repositoryMock = new Mock<IPedidoIntroducaoRepository>();
        private readonly Mock<IUserRepository> _repositoryUserMock = new Mock<IUserRepository>();
        private readonly Mock<ILigacaoRepository> _repositoryLigacaoMock = new Mock<ILigacaoRepository>();
        private readonly Mock<IPedidoLigacaoRepository> _repositoryPedidoLigacaoMock = new Mock<IPedidoLigacaoRepository>();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();


        public PedidoIntroducaoServiceTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _service = new PedidoIntroducaoService(_unitOfWorkMock.Object, _repositoryMock.Object,
                _repositoryUserMock.Object, _repositoryLigacaoMock.Object,_repositoryPedidoLigacaoMock.Object);
        }

        [Fact]
        public void GetAllAsyncTest()
        {
            PedidoIntroducaoId id = new PedidoIntroducaoId(new Guid());
            EstadoPedidoIntroducao estado = new EstadoPedidoIntroducao("PENDENTE");
            User remetente = new User(new UserId(new Guid()), new Email("user@gmail.com"), new UserName("userName"),
                new Password("pwd123A14"));
            User intermediario = new User(new UserId(new Guid()), new Email("user1@gmail.com"),
                new UserName("userName1"), new Password("pwd123A15"));
            User destinatario = new User(new UserId(new Guid()), new Email("user2@gmail.com"),
                new UserName("userName2"), new Password("pwd123A16"));
            Apresentacao apresentacao = new Apresentacao("Olá, pretendia ser introduzido.");
            Apresentacao apresentacaoLigacao = new Apresentacao("Olá, pretendia ligar-me a ti.");
            var list = new List<PedidoIntroducao>();
            list.Add(new PedidoIntroducao(id, estado, remetente, intermediario, destinatario, apresentacao,apresentacaoLigacao));
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var result = _service.GetAllAsync();
            List<PedidoIntroducaoDTO> resultDTO =
                list.ConvertAll<PedidoIntroducaoDTO>(pedido => PedidoIntroducaoDTOParser.ParaDTO(pedido));
            Assert.Equal(resultDTO.ToString(), result.Result.ToString());
        }

        [Fact]
        public void GetByIdTest()
        {
            PedidoIntroducaoId id = new PedidoIntroducaoId(new Guid());
            EstadoPedidoIntroducao estado = new EstadoPedidoIntroducao("PENDENTE");
            User remetente = new User(new UserId(new Guid()), new Email("user@gmail.com"), new UserName("userName"),
                new Password("pwd123A14"));
            User intermediario = new User(new UserId(new Guid()), new Email("user1@gmail.com"),
                new UserName("userName1"), new Password("pwd123A15"));
            User destinatario = new User(new UserId(new Guid()), new Email("user2@gmail.com"),
                new UserName("userName2"), new Password("pwd123A16"));
            Apresentacao apresentacao = new Apresentacao("Olá, pretendia ser introduzido.");
            Apresentacao apresentacaoLigacao = new Apresentacao("Olá, pretendia ligar-me a ti.");
            PedidoIntroducao pedidoIntroducao =
                new PedidoIntroducao(id, estado, remetente, intermediario, destinatario, apresentacao,apresentacaoLigacao);

            _repositoryMock.Setup(x => x.GetByIdAsync(pedidoIntroducao.Id)).ReturnsAsync(pedidoIntroducao);
            var result = _service.GetById(pedidoIntroducao.Id);
            PedidoIntroducaoDTO pedidoIntroducaoDTO = PedidoIntroducaoDTOParser.ParaDTO(pedidoIntroducao);

            pedidoIntroducaoDTO.Should().BeEquivalentTo(result.Result.Value);
        }

        [Fact]
        public void GetByIdTestNull()
        {
            _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<PedidoIntroducaoId>())).ReturnsAsync(() => null);
            var result = _service.GetById(new PedidoIntroducaoId(new Guid()));
            Assert.Null(result.Result);
        }

        [Fact]
        public void GetByEstadoPendenteTest()
        {
            PedidoIntroducaoId id = new PedidoIntroducaoId(new Guid());
            EstadoPedidoIntroducao estado = new EstadoPedidoIntroducao("PENDENTE");
            User remetente = new User(new UserId(new Guid()), new Email("user@gmail.com"), new UserName("userName"),
                new Password("pwd123A14"));
            User intermediario = new User(new UserId(new Guid()), new Email("user1@gmail.com"),
                new UserName("userName1"), new Password("pwd123A15"));
            User destinatario = new User(new UserId(new Guid()), new Email("user2@gmail.com"),
                new UserName("userName2"), new Password("pwd123A16"));
            Apresentacao apresentacao = new Apresentacao("Olá, pretendia ser introduzido.");
            Apresentacao apresentacaoLigacao = new Apresentacao("Olá, pretendia ligar-me a ti.");
            PedidoIntroducao pedidoIntroducao =
                new PedidoIntroducao(id, estado, remetente, intermediario, destinatario, apresentacao,apresentacaoLigacao);
            var list = new List<PedidoIntroducao>();
            list.Add(pedidoIntroducao);
            _repositoryMock.Setup(x => x.GetByEstadoPendenteAsync(estado.estadoPedidoIntroducao)).ReturnsAsync(list);
            var result = _service.GetByEstadoPendente(estado.estadoPedidoIntroducao);
            var listDTO = list.ConvertAll<PedidoIntroducaoDTO>(pedido => PedidoIntroducaoDTOParser.ParaDTO(pedido));

            listDTO.Should().BeEquivalentTo(result.Result);
        }

        [Fact]
        public void GetByEstadoPendenteTestNull()
        {
            _repositoryMock.Setup(x => x.GetByEstadoPendenteAsync(It.IsAny<String>())).ReturnsAsync(() => null);
            var result = _service.GetByEstadoPendente("7389f1d9-4d80-4bb3-9648-aa9fea779e8z");
            Assert.Null(result.Result);
        }

        [Fact]
        public void AddAsyncTest()
        {
            List<Ligacao> list = new List<Ligacao>();
            List<Ligacao> list2 = new List<Ligacao>();
            List<Ligacao> list3 = new List<Ligacao>();
            string estado = "PENDENTE";
            User remetente = new User(new UserId(Guid.NewGuid()), new Email("testrem@gmail.com"),
                new UserName("testrem"), new Password("test"));
            User intermediario = new User(new UserId(Guid.NewGuid()), new Email("testinter@gmail.com"),
                new UserName("testint"), new Password("test"));
            User destinatario = new User(new UserId(Guid.NewGuid()), new Email("testdest@gmail.com"),
                new UserName("testdest"), new Password("test"));
            String apresentacao = "Quero que aceites o pedido";
            String apresentacaoLigacao = "Olá, pretendia ligar-me a ti.";
            PedidoIntroducaoPutDTO pedidoPutDTO = new PedidoIntroducaoPutDTO(remetente.Username.userName,
                intermediario.Username.userName, destinatario.Username.userName, apresentacao,apresentacaoLigacao);

            CreatingPedidoIntroducaoDTO pedidoIntroducaoDto = new CreatingPedidoIntroducaoDTO(estado,
                new UserDTO(remetente.Id.AsGuid(), remetente.Email.EmailLogin, remetente.Username.userName,
                    remetente.Password.pwd),
                new UserDTO(intermediario.Id.AsGuid(), intermediario.Email.EmailLogin, intermediario.Username.userName,
                    intermediario.Password.pwd),
                new UserDTO(destinatario.Id.AsGuid(), destinatario.Email.EmailLogin, destinatario.Username.userName,
                    destinatario.Password.pwd), apresentacao,apresentacaoLigacao);

            var pedido = PedidoIntroducaoDTOParser.DeDTOSemID(pedidoIntroducaoDto);

            PedidoIntroducaoDTO pedidoDTO = PedidoIntroducaoDTOParser.ParaDTO(pedido);

            _repositoryUserMock.Setup(x => x.GetUserByUsername(remetente.Username.userName)).ReturnsAsync(remetente);
            _repositoryUserMock.Setup(x => x.GetUserByUsername(destinatario.Username.userName))
                .ReturnsAsync(destinatario);
            _repositoryUserMock.Setup(x => x.GetUserByUsername(intermediario.Username.userName))
                .ReturnsAsync(intermediario);

            Ligacao l1 = new Ligacao(new UserId(remetente.Id.Value), new UserId(intermediario.Id.Value),
                new ForçaLigacao(2), new List<Tag>());
            list.Add(l1);
            Ligacao l2 = new Ligacao(new UserId(intermediario.Id.Value), new UserId(destinatario.Id.Value),
                new ForçaLigacao(2), new List<Tag>());
            list2.Add(l2);

            _repositoryLigacaoMock.Setup(x => x.GetLigacaoByUsers(remetente.Id, intermediario.Id)).ReturnsAsync(list);
            _repositoryLigacaoMock.Setup(x => x.GetLigacaoByUsers(intermediario.Id, destinatario.Id))
                .ReturnsAsync(list2);
            _repositoryLigacaoMock.Setup(x => x.GetLigacaoByUsers(remetente.Id, destinatario.Id)).ReturnsAsync(list3);

            _repositoryMock.Setup(x => x.AddAsync(pedido)).ReturnsAsync(pedido);

            Task<PedidoIntroducaoDTO> result = _service.AddAsync(pedidoPutDTO);
            pedidoDTO.ToString().Should().BeEquivalentTo(result.Result.ToString());
        }

        [Fact]
        public void AddAsyncNullTest1()
        {
            List<Ligacao> list = new List<Ligacao>();
            List<Ligacao> list2 = new List<Ligacao>();
            List<Ligacao> list3 = new List<Ligacao>();
            string estado = "PENDENTE";
            User remetente = new User(new UserId(Guid.NewGuid()), new Email("testrem@gmail.com"),
                new UserName("testrem"), new Password("test"));
            User intermediario = new User(new UserId(Guid.NewGuid()), new Email("testinter@gmail.com"),
                new UserName("testint"), new Password("test"));
            User destinatario = new User(new UserId(Guid.NewGuid()), new Email("testdest@gmail.com"),
                new UserName("testdest"), new Password("test"));
            String apresentacao = "Quero que aceites o pedido";
            String apresentacaoLigacao = "Olá, pretendia ligar-me a ti.";
            PedidoIntroducaoPutDTO pedidoPutDTO = new PedidoIntroducaoPutDTO(remetente.Username.userName,
                intermediario.Username.userName, destinatario.Username.userName, apresentacao,apresentacaoLigacao);

            CreatingPedidoIntroducaoDTO pedidoIntroducaoDto = new CreatingPedidoIntroducaoDTO(estado,
                new UserDTO(remetente.Id.AsGuid(), remetente.Email.EmailLogin, remetente.Username.userName,
                    remetente.Password.pwd),
                new UserDTO(intermediario.Id.AsGuid(), intermediario.Email.EmailLogin, intermediario.Username.userName,
                    intermediario.Password.pwd),
                new UserDTO(destinatario.Id.AsGuid(), destinatario.Email.EmailLogin, destinatario.Username.userName,
                    destinatario.Password.pwd), apresentacao,apresentacaoLigacao);

            var pedido = PedidoIntroducaoDTOParser.DeDTOSemID(pedidoIntroducaoDto);


            _repositoryLigacaoMock.Setup(x => x.GetLigacaoByUsers(remetente.Id, intermediario.Id)).ReturnsAsync(list);
            _repositoryLigacaoMock.Setup(x => x.GetLigacaoByUsers(intermediario.Id, destinatario.Id))
                .ReturnsAsync(list2);
            _repositoryLigacaoMock.Setup(x => x.GetLigacaoByUsers(remetente.Id, destinatario.Id)).ReturnsAsync(list3);

            _repositoryMock.Setup(x => x.AddAsync(pedido)).ReturnsAsync(pedido);

            Task<PedidoIntroducaoDTO> result = _service.AddAsync(pedidoPutDTO);
            Assert.Null(result.Result);
        }

        [Fact]
        public void UpdateAsyncTest()
        {
            PedidoIntroducaoId id = new PedidoIntroducaoId("7389f1d9-4d80-4bb3-9648-aa9fea779e8f");
            EstadoPedidoIntroducao estado = new EstadoPedidoIntroducao("APROVADO");
            User remetente = new User(new UserId(new Guid()), new Email("user@gmail.com"), new UserName("userName"),
                new Password("pwd123A14"));
            User intermediario = new User(new UserId(new Guid()), new Email("user1@gmail.com"),
                new UserName("userName1"), new Password("pwd123A15"));
            User destinatario = new User(new UserId(new Guid()), new Email("user2@gmail.com"),
                new UserName("userName2"), new Password("pwd123A16"));
            Apresentacao apresentacao = new Apresentacao("Olá, pretendia ser introduzido.");
            Apresentacao apresentacaoLigacao = new Apresentacao("Olá, pretendia ligar-me a ti.");
            PedidoIntroducao pedidoIntroducao =
                new PedidoIntroducao(id, estado, remetente, intermediario, destinatario, apresentacao,apresentacaoLigacao);

            _repositoryMock.Setup(x => x.GetByIdAsync(new PedidoIntroducaoId("7389f1d9-4d80-4bb3-9648-aa9fea779e8f")))
                .ReturnsAsync(pedidoIntroducao);

            PedidoIntroducaoDTOParser.ParaDTO(pedidoIntroducao).Should()
                .BeEquivalentTo(_service.UpdateAsync(new Guid("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"), estado).Result);
        }
        
            [Fact]
            public void AddAsyncNullTest2()
            {
                
                string estado = "PENDENTE";
                User remetente = new User(new UserId(Guid.NewGuid()),new Email("testrem@gmail.com"),new UserName("testrem"),new Password("test"));
                User intermediario = new User(new UserId(Guid.NewGuid()),new Email("testinter@gmail.com"),new UserName("testint"),new Password("test"));
                User destinatario = new User(new UserId(Guid.NewGuid()),new Email("testdest@gmail.com"),new UserName("testdest"),new Password("test"));
                String apresentacao = "Quero que aceites o pedido";
                String apresentacaoLigacao = "Olá, pretendia ligar-me a ti.";
                PedidoIntroducaoPutDTO pedidoPutDTO = new PedidoIntroducaoPutDTO(remetente.Username.userName, intermediario.Username.userName, destinatario.Username.userName, apresentacao,apresentacaoLigacao);
    
                CreatingPedidoIntroducaoDTO pedidoIntroducaoDto = new CreatingPedidoIntroducaoDTO( estado,new UserDTO(remetente.Id.AsGuid(),remetente.Email.EmailLogin,remetente.Username.userName,remetente.Password.pwd), new UserDTO(intermediario.Id.AsGuid(),intermediario.Email.EmailLogin,intermediario.Username.userName,intermediario.Password.pwd), new UserDTO(destinatario.Id.AsGuid(),destinatario.Email.EmailLogin,destinatario.Username.userName,destinatario.Password.pwd), apresentacao,apresentacaoLigacao);
                
                var pedido = PedidoIntroducaoDTOParser.DeDTOSemID(pedidoIntroducaoDto);
                
    
                _repositoryUserMock.Setup(x => x.GetUserByUsername(remetente.Username.userName)).ReturnsAsync(remetente);
                _repositoryUserMock.Setup(x => x.GetUserByUsername(destinatario.Username.userName)).ReturnsAsync(destinatario);
                _repositoryUserMock.Setup(x => x.GetUserByUsername(intermediario.Username.userName)).ReturnsAsync(intermediario);
                
                _repositoryMock.Setup(x => x.AddAsync(pedido)).ReturnsAsync(pedido);
                
                Task<PedidoIntroducaoDTO> result = _service.AddAsync(pedidoPutDTO);
                Assert.Null(result.Result);
            }
            [Fact]
            public void UpdateAsyncNullTest()
            {
                PedidoIntroducaoId id = new PedidoIntroducaoId("7389f1d9-4d80-4bb3-9648-aa9fea779e8f");
                EstadoPedidoIntroducao estado = new EstadoPedidoIntroducao("APROVADO");
                User remetente = new User(new UserId(new Guid()), new Email("user@gmail.com"), new UserName("userName"),
                    new Password("pwd123A14"));
                User intermediario = new User(new UserId(new Guid()), new Email("user1@gmail.com"),
                    new UserName("userName1"), new Password("pwd123A15"));
                User destinatario = new User(new UserId(new Guid()), new Email("user2@gmail.com"),
                    new UserName("userName2"), new Password("pwd123A16"));
                Apresentacao apresentacao = new Apresentacao("Olá, pretendia ser introduzido.");
                Apresentacao apresentacaoLigacao = new Apresentacao("Olá, pretendia ligar-me a ti.");
                PedidoIntroducao pedidoIntroducao =
                    new PedidoIntroducao(id, estado, remetente, intermediario, destinatario, apresentacao,apresentacaoLigacao);

                _repositoryMock.Setup(x => x.GetByIdAsync(new PedidoIntroducaoId("7389f1d9-4d80-4bb3-9648-aa9fea779e8f")))
                    .ReturnsAsync(() => null);
                var result=_service.UpdateAsync(new Guid("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"), estado);
                Assert.Null(result.Result);
            }

            [Fact]
            public void GetPossibleIntermediaryUsersTest()
            {
                User remetente = new User(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"), new Email("user@gmail.com"), new UserName("userName"),
                    new Password("pwd123A14"));
                User intermediario = new User(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e80"), new Email("user1@gmail.com"),
                    new UserName("userName1"), new Password("pwd123A15"));
                User destinatario = new User(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e85"), new Email("user2@gmail.com"),
                    new UserName("userName2"), new Password("pwd123A16"));
                
                Ligacao ligacao1 = new Ligacao(remetente.Id, intermediario.Id, new ForçaLigacao(4), new List<Tag>());
                List<Ligacao> l1 = new List<Ligacao>();
                Ligacao ligacao2 = new Ligacao(destinatario.Id, intermediario.Id, new ForçaLigacao(4), new List<Tag>());
                List<Ligacao> l2 = new List<Ligacao>();
                l1.Add(ligacao1);
                l2.Add(ligacao2);
                _repositoryLigacaoMock.Setup(x =>
                        x.GetLigacoesbyUser(remetente.Id))
                    .ReturnsAsync(l1);
                _repositoryLigacaoMock.Setup(x =>
                        x.GetLigacoesbyUser(destinatario.Id))
                    .ReturnsAsync(l2);
                UserDTO userDto = UserDTOParser.ParaDTO(intermediario);
                List<UserDTO> listUserDTO = new List<UserDTO>();
                listUserDTO.Add(userDto);
                
                _repositoryUserMock.Setup(x=>x.GetByIdAsync(intermediario.Id)).ReturnsAsync(intermediario);
                
                var result = _service.GetPossibleIntermediaryUsers(remetente.Id,destinatario.Id);
                listUserDTO.Should()
                    .BeEquivalentTo(result.Result);
            }
            [Fact]
            public void GetPossibleIntermediaryUsersTest2()
            {
                User remetente = new User(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"), new Email("user@gmail.com"), new UserName("userName"),
                    new Password("pwd123A14"));
                User intermediario = new User(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e80"), new Email("user1@gmail.com"),
                    new UserName("userName1"), new Password("pwd123A15"));
                User destinatario = new User(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e85"), new Email("user2@gmail.com"),
                    new UserName("userName2"), new Password("pwd123A16"));
                
                Ligacao ligacao1 = new Ligacao(intermediario.Id, remetente.Id, new ForçaLigacao(4), new List<Tag>());
                List<Ligacao> l1 = new List<Ligacao>();
                Ligacao ligacao2 = new Ligacao(destinatario.Id, intermediario.Id, new ForçaLigacao(4), new List<Tag>());
                List<Ligacao> l2 = new List<Ligacao>();
                l1.Add(ligacao1);
                l2.Add(ligacao2);
                _repositoryLigacaoMock.Setup(x =>
                        x.GetLigacoesbyUser(remetente.Id))
                    .ReturnsAsync(l1);
                _repositoryLigacaoMock.Setup(x =>
                        x.GetLigacoesbyUser(destinatario.Id))
                    .ReturnsAsync(l2);
                UserDTO userDto = UserDTOParser.ParaDTO(intermediario);
                List<UserDTO> listUserDTO = new List<UserDTO>();
                listUserDTO.Add(userDto);
                
                _repositoryUserMock.Setup(x=>x.GetByIdAsync(intermediario.Id)).ReturnsAsync(intermediario);
                
                var result = _service.GetPossibleIntermediaryUsers(remetente.Id,destinatario.Id);
                listUserDTO.Should()
                    .BeEquivalentTo(result.Result);
            }
    }
    }