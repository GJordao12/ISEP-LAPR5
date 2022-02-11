
/*
using System;
using System.Collections.Generic;
using DDDSample1.Domain.Perfis;
using DDDSample1.Domain.Pesquisa;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;
using FluentAssertions;
using Moq;
using Xunit;

namespace MasterDataRedeSocialTestes
{
    public class PesquisaServiceTest
    {
        private readonly PesquisaService _service;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock= new Mock<IUnitOfWork>();
        private readonly Mock<IUserRepository> _repoUserMock;
        private readonly Mock<IPerfilRepository>_repoPerfilMock;
        private readonly Mock<ITagRepository> _repoTagMock;
        
        public PesquisaServiceTest()
        {
            _repoUserMock=new Mock<IUserRepository>();
            _repoPerfilMock = new Mock<IPerfilRepository>();
            _repoTagMock = new Mock<ITagRepository>();
            _service = new PesquisaService(_unitOfWorkMock.Object, _repoUserMock.Object,_repoPerfilMock.Object,_repoTagMock.Object);

        }
        
        
        [Fact]
        public void GetUsersWithSearchFieldTestEmail()
        {
            User u1 = new User(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"), new Email("user@gmail.com"), new UserName("userName"),
                new Password("pwd123A14"));
            User u2 = new User(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e80"), new Email("user1@gmail.com"),
                new UserName("userName1"), new Password("pwd123A15"));
            String email = "user1@gmail.com";
            List < User > list1= new List<User>();
            list1.Add(u2);
            _repoUserMock.Setup(x => x.GetUsersWithSearchEmailFieldAsync(u1.Id,email))
                .ReturnsAsync(list1);
            List<UserDTO> listUserDTO = new List<UserDTO>();
            listUserDTO.Add(UserDTOParser.ParaDTO(u2));
            var result=_service.GetUsersWithSearchField(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"),null,email,null );
            listUserDTO.Should().BeEquivalentTo(result.Result);
        }
        
        [Fact]
        public void GetUsersWithSearchFieldTestNome()
        {
            User u1 = new User(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"), new Email("user@gmail.com"), new UserName("userName"),
                new Password("pwd123A14"));
            User u2 = new User(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e80"), new Email("user1@gmail.com"),
                new UserName("userName1"), new Password("pwd123A15"));
            String nome = "userName1";
            List < User > list1= new List<User>();
            list1.Add(u2);
            _repoUserMock.Setup(x => x.GetUsersWithSearchNameFieldAsync(u1.Id,nome))
                .ReturnsAsync(list1);
            List<UserDTO> listUserDTO = new List<UserDTO>();
            listUserDTO.Add(UserDTOParser.ParaDTO(u2));
            var result=_service.GetUsersWithSearchField(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"),nome,null,null );
            listUserDTO.Should().BeEquivalentTo(result.Result);
        }
        
        [Fact]
        public void GetUsersWithSearchFieldTestTag()
        {
            User u1 = new User(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"), new Email("user@gmail.com"), new UserName("userName"),
                new Password("pwd123A14"));
            User u2 = new User(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e80"), new Email("user1@gmail.com"),
                new UserName("userName1"), new Password("pwd123A15"));
            Tag tag = new Tag(new TagID("7389f1d9-4d80-4bb3-9648-aa9fea779e81"),new Nome("java"));
            
            List<Tag> listTags = new List<Tag>();
            listTags.Add(tag);
            
            Perfil p2 = new Perfil(new PerfilId("7389f1d9-4d80-4bb3-9648-aa9fea779e80"), new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e80"),null, null, null, null, null, null, listTags);
            List<Perfil> perfillist = new List<Perfil>();
            perfillist.Add(p2);
            
            
            List < User > list1= new List<User>();
            list1.Add(u2);
            _repoTagMock.Setup(x => x.GetByNomeAsync(tag.nome.nome)).ReturnsAsync(listTags);
            _repoPerfilMock.Setup(x => x.GetUsersWithSearchTagFieldAsync(u1.Id,listTags[0]))
                .ReturnsAsync(perfillist);

            _repoUserMock.Setup(x => x.GetByIdAsync(p2.UserId)).ReturnsAsync(u2);
            List<UserDTO> listUserDTO = new List<UserDTO>();
            listUserDTO.Add(UserDTOParser.ParaDTO(u2));
            var result=_service.GetUsersWithSearchField(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"),null,null,tag.nome.nome );
            listUserDTO.Should().BeEquivalentTo(result.Result);
        }
        
        
    }
}*/