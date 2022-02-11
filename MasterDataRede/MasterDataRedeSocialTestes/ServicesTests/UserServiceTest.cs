using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Perfis;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Utilizador;
using FluentAssertions;
using Moq;
using Xunit;

namespace MasterDataRedeSocialTestes
{
    public class UserServiceTest
    {
        private readonly UserService _service;
        private readonly Mock<IUserRepository> _repositoryMock = new Mock<IUserRepository>();
        private readonly Mock<IPerfilRepository> _repositoryPerfilMock = new Mock<IPerfilRepository>();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        public UserServiceTest()
        {
            _service = new UserService(_unitOfWorkMock.Object, _repositoryMock.Object, _repositoryPerfilMock.Object);
        }
        
        [Fact]
        public void GetAllAsyncTest()
        {
            var list = new List<User>();
            list.Add(new User(new UserId(new Guid()), new Email("utilizador@hotmail.com"), new UserName("userName10"), new Password("&Pwd123456789")));
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            List<UserDTO> resultDTO = list.ConvertAll<UserDTO>(user => UserDTOParser.ParaDTO(user));
            var result =  _service.GetAllAsync();
            resultDTO.Should().BeEquivalentTo(result.Result);
        }
        
        [Fact]
        public void GetByIdTest()
        {
            User user = new User(new UserId(new Guid()), new Email("utilizador@hotmail.com"), new UserName("userName10"), new Password("&Pwd123456789"));

            _repositoryMock.Setup(x => x.GetByIdAsync(user.Id)).ReturnsAsync(user);
            var result =  _service.GetById(user.Id);
            UserDTO UserDTO = UserDTOParser.ParaDTO(user);
            
            UserDTO.Should().BeEquivalentTo(result.Result);
        }
        
        [Fact]
        public void GetByIdTestNull()
        {
            _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<UserId>())).ReturnsAsync(() => null);
            var result =  _service.GetById(new UserId(new Guid()));
            Assert.Null(result.Result);
        }
        
        [Fact]
        public void AddAsyncTest()
        {

            CreatingUserDTO user = new CreatingUserDTO("email@gmail.com", "username", "pwd123A2222");

            var userNovo = UserDTOParser.DeDTOSemID(user);
            
            UserDTO userDTO = UserDTOParser.ParaDTOSemPass(userNovo);
            
            _repositoryMock.Setup(x => x.AddAsync(userNovo)).ReturnsAsync(userNovo);
            
            Task<UserDTO> result = _service.AddAsync(user);

            userDTO.Username.Should().BeEquivalentTo(result.Result.Username);
        }
    }
}