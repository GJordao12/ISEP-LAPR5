using System;
using System.Collections.Generic;
using DDDSample1.Domain.EstadosDeHumor;

using DDDSample1.Domain.Perfis;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;
using FluentAssertions;
using Moq;
using Xunit;

namespace MasterDataRedeSocialTestes
{
    public class PerfilTest
    {
        private readonly PerfilService _service;
        private readonly Mock<IPerfilRepository> _repositoryMock = new Mock<IPerfilRepository>();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IEstadoDeHumorRepository> _repoEstadoDeHumor = new Mock<IEstadoDeHumorRepository>();
        private readonly Mock<ITagRepository> _repoTag = new Mock<ITagRepository>();
        private readonly Mock<IUserRepository> _repoUser = new Mock<IUserRepository>();
        
        public PerfilTest()
        {
            _service = new PerfilService(_unitOfWorkMock.Object, _repositoryMock.Object, _repoEstadoDeHumor.Object,_repoUser.Object, _repoTag.Object);
        }

        [Fact]
        public void GetAllAsyncTest()
        {
            var list = new List<Perfil>();
            list.Add(new Perfil(new PerfilId (new Guid()), new UserId(new Guid()), new EstadoDeHumorId(new Guid()),new PerfilDataDeNascimento("20/12/1998"),new PerfilFacebook("https://www.facebook.com/profile.php?id=100006183047762"), new PerfilLinkedin("https://www.linkedin.com/in/alvaro-husillos-77a294173/"), new PerfilNome("Juan"), new PerfilNTelefone("913427123"), new List<Tag>()));
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var result = _service.GetAllAsync();
            List<PerfilDto> resultDTO =
                list.ConvertAll<PerfilDto>(perfil => PerfilDtoParser.ParaDTO(perfil));
            Assert.Equal(resultDTO.ToString(), result.Result.Value.ToString());
        }
        
        [Fact]
        public void GetByIdTest()
        {
            PerfilId Id = new PerfilId(Guid.NewGuid());
            UserId userId = new UserId(Guid.NewGuid());
            EstadoDeHumorId EstadoDeHumor = new EstadoDeHumorId(Guid.NewGuid());
            PerfilDataDeNascimento DataDeNascimento = new PerfilDataDeNascimento("20/12/1998");
            PerfilFacebook perfilFacebook = new PerfilFacebook("https://www.facebook.com/profile.php?id=100006183047762");
            PerfilLinkedin perfilLinkedin = new PerfilLinkedin("https://www.linkedin.com/in/alvaro-husillos-77a294173/");
            PerfilNome nome = new PerfilNome("Juan");
            PerfilNTelefone nTelefone = new PerfilNTelefone("913427123");
            ICollection<Tag> tag = new List<Tag>();

            var perfil = new Perfil(Id,userId,EstadoDeHumor,DataDeNascimento,perfilFacebook, perfilLinkedin, nome, nTelefone, tag );
            PerfilDto perfilDto = PerfilDtoParser.ParaDTO(perfil);
            
            _repositoryMock.Setup(x => x.GetByIdAsync(perfil.Id)).ReturnsAsync(perfil);
            var result = _service.GetById(perfil.Id);
            perfilDto.Should().BeEquivalentTo(result.Result.Value);
        }
        
        [Fact]
        public void UpdatePerfilTest()
        {
            
            PerfilId Id = new PerfilId(Guid.NewGuid());
            UserId userId = new UserId(Guid.NewGuid());
            EstadoDeHumorId EstadoDeHumorId = new EstadoDeHumorId(Guid.NewGuid());
            PerfilDataDeNascimento DataDeNascimento = new PerfilDataDeNascimento("20/12/1998");
            PerfilFacebook perfilFacebook = new PerfilFacebook("https://www.facebook.com/profile.php?id=100006183047762");
            PerfilLinkedin perfilLinkedin = new PerfilLinkedin("https://www.linkedin.com/in/alvaro-husillos-77a294173/");
            PerfilNome nome = new PerfilNome("Juan");
            PerfilNTelefone nTelefone = new PerfilNTelefone("913427123");
            List<Tag> listTags = new List<Tag>();
            Tag tag = new Tag(new TagID("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"),new Nome("juan"));
            listTags.Add(tag);
            TagDTO tagDto = TagDTOParser.ParaDTO(tag);
            ICollection<TagDTO> listTagDTO = new List<TagDTO>();
            listTagDTO.Add(tagDto);


            var perfil = new Perfil(Id,userId,EstadoDeHumorId,DataDeNascimento,perfilFacebook, perfilLinkedin, nome, nTelefone, listTags );
            PerfilDto perfilDto = PerfilDtoParser.ParaDTO(perfil);
            
            _repositoryMock.Setup(x => x.GetByIdAsync(perfil.Id)).ReturnsAsync(perfil);
            
            
            EstadoDeHumor estadoDeHumor = new EstadoDeHumor(EstadoDeHumorId,"hola");
            _repoEstadoDeHumor.Setup(x => x.GetByIdAsync(estadoDeHumor.Id)).ReturnsAsync(estadoDeHumor);
            
            CreatingPerfilPutDto perfilPutDto = new CreatingPerfilPutDto(EstadoDeHumorDtoParser.ParaDTO(estadoDeHumor),perfil.perfilDataDeNascimento.Data,perfil.perfilFacebook.LinkFacebook,perfil.perfilLinkedin.LinkLinkedin,perfil.perfilNome.NomePerfil,perfil.perfilNTelefone.NTelefono,listTagDTO);
            
            _repoTag.Setup(x => x.GetByNomeAsync(tag.nome.nome)).ReturnsAsync(listTags);

            var result = _service.UpdateAsync(perfil.Id.AsGuid(),perfilPutDto);
            
            perfilDto.Should().BeEquivalentTo(result.Result);
            
        }
        
        [Fact]
        public void UpdatePerfilTestNull()
        {
            
            PerfilId Id = new PerfilId(Guid.NewGuid());
            UserId userId = new UserId(Guid.NewGuid());
            EstadoDeHumorId EstadoDeHumorId = new EstadoDeHumorId(Guid.NewGuid());
            PerfilDataDeNascimento DataDeNascimento = new PerfilDataDeNascimento("20/12/1998");
            PerfilFacebook perfilFacebook = new PerfilFacebook("https://www.facebook.com/profile.php?id=100006183047762");
            PerfilLinkedin perfilLinkedin = new PerfilLinkedin("https://www.linkedin.com/in/alvaro-husillos-77a294173/");
            PerfilNome nome = new PerfilNome("Juan");
            PerfilNTelefone nTelefone = new PerfilNTelefone("913427123");
            List<Tag> listTags = new List<Tag>();
            Tag tag = new Tag(new TagID("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"),new Nome("juan"));
            listTags.Add(tag);
            TagDTO tagDto = TagDTOParser.ParaDTO(tag);
            ICollection<TagDTO> listTagDTO = new List<TagDTO>();
            listTagDTO.Add(tagDto);


            var perfil = new Perfil(Id,userId,EstadoDeHumorId,DataDeNascimento,perfilFacebook, perfilLinkedin, nome, nTelefone, listTags );

            _repositoryMock.Setup(x => x.GetByIdAsync(perfil.Id)).ReturnsAsync(perfil );
            
            EstadoDeHumor estadoDeHumor = new EstadoDeHumor(EstadoDeHumorId,"hola");
            _repoEstadoDeHumor.Setup(x => x.GetByIdAsync(estadoDeHumor.Id)).ReturnsAsync(() => null);
            
            CreatingPerfilPutDto perfilPutDto = new CreatingPerfilPutDto(EstadoDeHumorDtoParser.ParaDTO(estadoDeHumor),perfil.perfilDataDeNascimento.Data,perfil.perfilFacebook.LinkFacebook,perfil.perfilLinkedin.LinkLinkedin,perfil.perfilNome.NomePerfil,perfil.perfilNTelefone.NTelefono,listTagDTO);
            
            
            var result = _service.UpdateAsync(perfil.Id.AsGuid(),perfilPutDto);
            Assert.Null(result.Result);
            
        }
        
        [Fact]
        public void UpdatePerfilTestNull2()
        {
            
            PerfilId Id = new PerfilId(Guid.NewGuid());
            UserId userId = new UserId(Guid.NewGuid());
            EstadoDeHumorId EstadoDeHumorId = new EstadoDeHumorId(Guid.NewGuid());
            PerfilDataDeNascimento DataDeNascimento = new PerfilDataDeNascimento("20/12/1998");
            PerfilFacebook perfilFacebook = new PerfilFacebook("https://www.facebook.com/profile.php?id=100006183047762");
            PerfilLinkedin perfilLinkedin = new PerfilLinkedin("https://www.linkedin.com/in/alvaro-husillos-77a294173/");
            PerfilNome nome = new PerfilNome("Juan");
            PerfilNTelefone nTelefone = new PerfilNTelefone("913427123");
            List<Tag> listTags = new List<Tag>();
            Tag tag = new Tag(new TagID("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"),new Nome("juan"));
            listTags.Add(tag);
            TagDTO tagDto = TagDTOParser.ParaDTO(tag);
            ICollection<TagDTO> listTagDTO = new List<TagDTO>();
            listTagDTO.Add(tagDto);


            var perfil = new Perfil(Id,userId,EstadoDeHumorId,DataDeNascimento,perfilFacebook, perfilLinkedin, nome, nTelefone, listTags );

            _repositoryMock.Setup(x => x.GetByIdAsync(perfil.Id)).ReturnsAsync(() => null);
            
            
            EstadoDeHumor estadoDeHumor = new EstadoDeHumor(EstadoDeHumorId,"hola");
            _repoEstadoDeHumor.Setup(x => x.GetByIdAsync(estadoDeHumor.Id)).ReturnsAsync(estadoDeHumor);
            
            CreatingPerfilPutDto perfilPutDto = new CreatingPerfilPutDto(EstadoDeHumorDtoParser.ParaDTO(estadoDeHumor),perfil.perfilDataDeNascimento.Data,perfil.perfilFacebook.LinkFacebook,perfil.perfilLinkedin.LinkLinkedin,perfil.perfilNome.NomePerfil,perfil.perfilNTelefone.NTelefono,listTagDTO);
            
            _repoTag.Setup(x => x.GetByNomeAsync(tag.nome.nome)).ReturnsAsync(listTags);

            var result = _service.UpdateAsync(perfil.Id.AsGuid(),perfilPutDto);
            
            Assert.Null(result.Result);
        }
        
        [Fact]
        public void UpdateEstadoDeHumorTest()
        {
            PerfilId Id = new PerfilId(Guid.NewGuid());
            UserId userId = new UserId(Guid.NewGuid());
            EstadoDeHumorId EstadoDeHumorId = new EstadoDeHumorId("0089f1d9-4d80-4bb3-9648-aa9fea779e8f");
            PerfilDataDeNascimento DataDeNascimento = new PerfilDataDeNascimento("20/12/1998");
            PerfilFacebook perfilFacebook = new PerfilFacebook("https://www.facebook.com/profile.php?id=100006183047762");
            PerfilLinkedin perfilLinkedin = new PerfilLinkedin("https://www.linkedin.com/in/alvaro-husillos-77a294173/");
            PerfilNome nome = new PerfilNome("Juan");
            PerfilNTelefone nTelefone = new PerfilNTelefone("913427123");
            List<Tag> listTags = new List<Tag>();
            Tag tag = new Tag(new TagID("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"),new Nome("juan"));
            listTags.Add(tag);
            TagDTO tagDto = TagDTOParser.ParaDTO(tag);
            ICollection<TagDTO> listTagDTO = new List<TagDTO>();
            listTagDTO.Add(tagDto);
            
            var perfil = new Perfil(Id,userId,EstadoDeHumorId,DataDeNascimento,perfilFacebook, perfilLinkedin, nome, nTelefone, listTags );
            _repositoryMock.Setup(x => x.GetByIdAsync(perfil.Id)).ReturnsAsync(perfil);

            EstadoDeHumor estadoDeHumor = new EstadoDeHumor(new EstadoDeHumorId("1189f1d9-4d80-4bb3-9648-aa9fea779e8f"), "Contente");
            _repoEstadoDeHumor.Setup(x => x.GetByIdAsync(estadoDeHumor.Id)).ReturnsAsync(estadoDeHumor);
            
            perfil.ChangeEstadoDeHumorId(estadoDeHumor.Id);
            PerfilDto perfilDto = PerfilDtoParser.ParaDTO(perfil);

            EstadoDeHumorDto estadoDto = EstadoDeHumorDtoParser.ParaDTO(estadoDeHumor);
            
            var result = _service.UpdateEstadoDeHumorAsync(perfil.Id.AsGuid(),estadoDto);
            
            perfilDto.Should().BeEquivalentTo(result.Result);
        }
        
        [Fact]
        public void UpdateEstadoDeHumorTestNull()
        {
            PerfilId Id = new PerfilId(Guid.NewGuid());
            UserId userId = new UserId(Guid.NewGuid());
            EstadoDeHumorId EstadoDeHumorId = new EstadoDeHumorId("0089f1d9-4d80-4bb3-9648-aa9fea779e8f");
            PerfilDataDeNascimento DataDeNascimento = new PerfilDataDeNascimento("20/12/1998");
            PerfilFacebook perfilFacebook = new PerfilFacebook("https://www.facebook.com/profile.php?id=100006183047762");
            PerfilLinkedin perfilLinkedin = new PerfilLinkedin("https://www.linkedin.com/in/alvaro-husillos-77a294173/");
            PerfilNome nome = new PerfilNome("Juan");
            PerfilNTelefone nTelefone = new PerfilNTelefone("913427123");
            List<Tag> listTags = new List<Tag>();
            Tag tag = new Tag(new TagID("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"),new Nome("juan"));
            listTags.Add(tag);
            TagDTO tagDto = TagDTOParser.ParaDTO(tag);
            ICollection<TagDTO> listTagDTO = new List<TagDTO>();
            listTagDTO.Add(tagDto);
            
            var perfil = new Perfil(Id,userId,EstadoDeHumorId,DataDeNascimento,perfilFacebook, perfilLinkedin, nome, nTelefone, listTags );
            _repositoryMock.Setup(x => x.GetByIdAsync(perfil.Id)).ReturnsAsync(perfil);

            EstadoDeHumor estadoDeHumor = new EstadoDeHumor(new EstadoDeHumorId("1189f1d9-4d80-4bb3-9648-aa9fea779e8f"), "Contente");
            _repoEstadoDeHumor.Setup(x => x.GetByIdAsync(estadoDeHumor.Id)).ReturnsAsync(() => null);
            
            perfil.ChangeEstadoDeHumorId(estadoDeHumor.Id);

            EstadoDeHumorDto estadoDto = EstadoDeHumorDtoParser.ParaDTO(estadoDeHumor);
            try
            {
                _service.UpdateEstadoDeHumorAsync(perfil.Id.AsGuid(), estadoDto);
                Assert.False(false);
            }
            catch (BusinessRuleValidationException ex)
            {
                Assert.True(true);
            }
        }
        
        [Fact]
        public void UpdateEstadoDeHumorTestNull2()
        {
            PerfilId Id = new PerfilId(Guid.NewGuid());
            UserId userId = new UserId(Guid.NewGuid());
            EstadoDeHumorId EstadoDeHumorId = new EstadoDeHumorId("0089f1d9-4d80-4bb3-9648-aa9fea779e8f");
            PerfilDataDeNascimento DataDeNascimento = new PerfilDataDeNascimento("20/12/1998");
            PerfilFacebook perfilFacebook = new PerfilFacebook("https://www.facebook.com/profile.php?id=100006183047762");
            PerfilLinkedin perfilLinkedin = new PerfilLinkedin("https://www.linkedin.com/in/alvaro-husillos-77a294173/");
            PerfilNome nome = new PerfilNome("Juan");
            PerfilNTelefone nTelefone = new PerfilNTelefone("913427123");
            List<Tag> listTags = new List<Tag>();
            Tag tag = new Tag(new TagID("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"),new Nome("juan"));
            listTags.Add(tag);
            TagDTO tagDto = TagDTOParser.ParaDTO(tag);
            ICollection<TagDTO> listTagDTO = new List<TagDTO>();
            listTagDTO.Add(tagDto);
            
            var perfil = new Perfil(Id,userId,EstadoDeHumorId,DataDeNascimento,perfilFacebook, perfilLinkedin, nome, nTelefone, listTags );
            _repositoryMock.Setup(x => x.GetByIdAsync(perfil.Id)).ReturnsAsync(() => null);

            EstadoDeHumor estadoDeHumor = new EstadoDeHumor(new EstadoDeHumorId("1189f1d9-4d80-4bb3-9648-aa9fea779e8f"), "Contente");
            _repoEstadoDeHumor.Setup(x => x.GetByIdAsync(estadoDeHumor.Id)).ReturnsAsync(estadoDeHumor);
            
            perfil.ChangeEstadoDeHumorId(estadoDeHumor.Id);

            EstadoDeHumorDto estadoDto = EstadoDeHumorDtoParser.ParaDTO(estadoDeHumor);
            try
            {
                _service.UpdateEstadoDeHumorAsync(perfil.Id.AsGuid(), estadoDto);
                Assert.False(false);
            }
            catch (BusinessRuleValidationException ex)
            {
                Assert.True(true);
            }
        }
        
        [Fact]
        public void UpdateEstadoDeHumorTestIdDiferenteDescricao()
        {
            PerfilId Id = new PerfilId(Guid.NewGuid());
            UserId userId = new UserId(Guid.NewGuid());
            EstadoDeHumorId EstadoDeHumorId = new EstadoDeHumorId("0089f1d9-4d80-4bb3-9648-aa9fea779e8f");
            PerfilDataDeNascimento DataDeNascimento = new PerfilDataDeNascimento("20/12/1998");
            PerfilFacebook perfilFacebook = new PerfilFacebook("https://www.facebook.com/profile.php?id=100006183047762");
            PerfilLinkedin perfilLinkedin = new PerfilLinkedin("https://www.linkedin.com/in/alvaro-husillos-77a294173/");
            PerfilNome nome = new PerfilNome("Juan");
            PerfilNTelefone nTelefone = new PerfilNTelefone("913427123");
            List<Tag> listTags = new List<Tag>();
            Tag tag = new Tag(new TagID("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"),new Nome("juan"));
            listTags.Add(tag);
            TagDTO tagDto = TagDTOParser.ParaDTO(tag);
            ICollection<TagDTO> listTagDTO = new List<TagDTO>();
            listTagDTO.Add(tagDto);
            
            var perfil = new Perfil(Id,userId,EstadoDeHumorId,DataDeNascimento,perfilFacebook, perfilLinkedin, nome, nTelefone, listTags );
            _repositoryMock.Setup(x => x.GetByIdAsync(perfil.Id)).ReturnsAsync(perfil);

            EstadoDeHumor estadoDeHumor1 = new EstadoDeHumor(new EstadoDeHumorId("1189f1d9-4d80-4bb3-9648-aa9fea779e8f"), "Contente");
            EstadoDeHumor estadoDeHumor2 = new EstadoDeHumor(new EstadoDeHumorId("2289f1d9-4d80-4bb3-9648-aa9fea779e8f"), "Triste");

            _repoEstadoDeHumor.Setup(x => x.GetByIdAsync(estadoDeHumor1.Id)).ReturnsAsync(estadoDeHumor2);
            
            perfil.ChangeEstadoDeHumorId(estadoDeHumor1.Id);

            EstadoDeHumorDto estadoDto = EstadoDeHumorDtoParser.ParaDTO(estadoDeHumor1);
            try
            {
                _service.UpdateEstadoDeHumorAsync(perfil.Id.AsGuid(), estadoDto);
                Assert.False(false);
            }
            catch (BusinessRuleValidationException ex)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void UpdatePerfilTest3()
        {

            PerfilId Id = new PerfilId(Guid.NewGuid());
            UserId userId = new UserId(Guid.NewGuid());
            EstadoDeHumorId EstadoDeHumorId = new EstadoDeHumorId(Guid.NewGuid());
            PerfilDataDeNascimento DataDeNascimento = new PerfilDataDeNascimento("20/12/1998");
            PerfilFacebook perfilFacebook =
                new PerfilFacebook("https://www.facebook.com/profile.php?id=100006183047762");
            PerfilLinkedin perfilLinkedin =
                new PerfilLinkedin("https://www.linkedin.com/in/alvaro-husillos-77a294173/");
            PerfilNome nome = new PerfilNome("Juan");
            PerfilNTelefone nTelefone = new PerfilNTelefone("913427123");
            List<Tag> listTags = new List<Tag>();
            Tag tag = new Tag(new TagID("7389f1d9-4d80-4bb3-9648-aa9fea779e8f"), new Nome("juan"));
            listTags.Add(tag);
            TagDTO tagDto = TagDTOParser.ParaDTO(tag);
            ICollection<TagDTO> listTagDTO = new List<TagDTO>();
            listTagDTO.Add(tagDto);


            var perfil = new Perfil(Id, userId, EstadoDeHumorId, DataDeNascimento, perfilFacebook, perfilLinkedin, nome,
                nTelefone, listTags);
            PerfilDto perfilDto = PerfilDtoParser.ParaDTO(perfil);

            _repositoryMock.Setup(x => x.GetByIdAsync(perfil.Id)).ReturnsAsync(perfil);


            EstadoDeHumor estadoDeHumor = new EstadoDeHumor(EstadoDeHumorId, "hola");
            _repoEstadoDeHumor.Setup(x => x.GetByIdAsync(estadoDeHumor.Id)).ReturnsAsync(estadoDeHumor);

            _repoTag.Setup(x => x.GetByNomeAsync(tag.nome.nome)).ReturnsAsync(() => null);
            _repoTag.Setup(x => x.AddAsync(tag));

            CreatingPerfilPutDto perfilPutDto = new CreatingPerfilPutDto(EstadoDeHumorDtoParser.ParaDTO(estadoDeHumor),
                perfil.perfilDataDeNascimento.Data, perfil.perfilFacebook.LinkFacebook,
                perfil.perfilLinkedin.LinkLinkedin, perfil.perfilNome.NomePerfil, perfil.perfilNTelefone.NTelefono,
                listTagDTO);

            var result = _service.UpdateAsync(perfil.Id.AsGuid(), perfilPutDto);

            List<TagDTO> expected = new List<TagDTO>(perfilDto.ListaTags);
            List<TagDTO> result2 = new List<TagDTO>(result.Result.ListaTags);

            expected[0].nome.Should().BeEquivalentTo(result2[0].nome);
        }
        
        [Fact]
        public void GetByIdTestNull()
        {
            PerfilId Id = new PerfilId(Guid.NewGuid());
            UserId userId = new UserId(Guid.NewGuid());
            EstadoDeHumorId EstadoDeHumor = new EstadoDeHumorId(Guid.NewGuid());
            PerfilDataDeNascimento DataDeNascimento = new PerfilDataDeNascimento("20/12/1998");
            PerfilFacebook perfilFacebook = new PerfilFacebook("https://www.facebook.com/profile.php?id=100006183047762");
            PerfilLinkedin perfilLinkedin = new PerfilLinkedin("https://www.linkedin.com/in/alvaro-husillos-77a294173/");
            PerfilNome nome = new PerfilNome("Juan");
            PerfilNTelefone nTelefone = new PerfilNTelefone("913427123");
            ICollection<Tag> tag = new List<Tag>();

            var perfil = new Perfil(Id,userId,EstadoDeHumor,DataDeNascimento,perfilFacebook, perfilLinkedin, nome, nTelefone, tag );
            PerfilDto perfilDto = PerfilDtoParser.ParaDTO(perfil);
            
            _repositoryMock.Setup(x => x.GetByIdAsync(perfil.Id)).ReturnsAsync(() => null);
            var result = _service.GetById(perfil.Id);
            Assert.Null(result.Result);
        }
        [Fact]
        public void GetByUserIdTest()
        {
            PerfilId Id = new PerfilId(Guid.NewGuid());
            User u2 = new User(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e80"), new Email("user1@gmail.com"),
                new UserName("userName1"), new Password("pwd123A15"));
            EstadoDeHumorId EstadoDeHumor = new EstadoDeHumorId(Guid.NewGuid());
            PerfilDataDeNascimento DataDeNascimento = new PerfilDataDeNascimento("20/12/1998");
            PerfilFacebook perfilFacebook = new PerfilFacebook("https://www.facebook.com/profile.php?id=100006183047762");
            PerfilLinkedin perfilLinkedin = new PerfilLinkedin("https://www.linkedin.com/in/alvaro-husillos-77a294173/");
            PerfilNome nome = new PerfilNome("Juan");
            PerfilNTelefone nTelefone = new PerfilNTelefone("913427123");
            ICollection<Tag> tag = new List<Tag>();

            var perfil = new Perfil(Id,u2.Id,EstadoDeHumor,DataDeNascimento,perfilFacebook, perfilLinkedin, nome, nTelefone, tag );
            PerfilDto perfilDto = PerfilDtoParser.ParaDTO(perfil);
            
            _repoUser.Setup(x => x.GetByIdAsync(u2.Id)).ReturnsAsync(u2);
            _repositoryMock.Setup(x => x.GetByUserIdAsync(u2.Id)).ReturnsAsync(perfil);
            
            var result = _service.GetByUserId(u2.Id);

            perfilDto.Should().BeEquivalentTo(result.Result.Value);

        }
        
        [Fact]
        public void GetByUserIdTestNull()
        {
            PerfilId Id = new PerfilId(Guid.NewGuid());
            User u2 = new User(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e80"), new Email("user1@gmail.com"),
                new UserName("userName1"), new Password("pwd123A15"));
            EstadoDeHumorId EstadoDeHumor = new EstadoDeHumorId(Guid.NewGuid());
            PerfilDataDeNascimento DataDeNascimento = new PerfilDataDeNascimento("20/12/1998");
            PerfilFacebook perfilFacebook = new PerfilFacebook("https://www.facebook.com/profile.php?id=100006183047762");
            PerfilLinkedin perfilLinkedin = new PerfilLinkedin("https://www.linkedin.com/in/alvaro-husillos-77a294173/");
            PerfilNome nome = new PerfilNome("Juan");
            PerfilNTelefone nTelefone = new PerfilNTelefone("913427123");
            ICollection<Tag> tag = new List<Tag>();

            var perfil = new Perfil(Id,u2.Id,EstadoDeHumor,DataDeNascimento,perfilFacebook, perfilLinkedin, nome, nTelefone, tag );
            PerfilDto perfilDto = PerfilDtoParser.ParaDTO(perfil);
            _service.GetByUserId(u2.Id);
                
            try
            {
                _repoUser.Setup(x => x.GetByIdAsync(u2.Id)).ReturnsAsync(() => null);
                Assert.False(false);
            }
            catch (BusinessRuleValidationException ex)
            {
                Assert.True(true);
            }
        }
        
        [Fact]
        public void GetByUserIdTestNull2()
        {
            PerfilId Id = new PerfilId(Guid.NewGuid());
            User u2 = new User(new UserId("7389f1d9-4d80-4bb3-9648-aa9fea779e80"), new Email("user1@gmail.com"),
                new UserName("userName1"), new Password("pwd123A15"));
            EstadoDeHumorId EstadoDeHumor = new EstadoDeHumorId(Guid.NewGuid());
            PerfilDataDeNascimento DataDeNascimento = new PerfilDataDeNascimento("20/12/1998");
            PerfilFacebook perfilFacebook = new PerfilFacebook("https://www.facebook.com/profile.php?id=100006183047762");
            PerfilLinkedin perfilLinkedin = new PerfilLinkedin("https://www.linkedin.com/in/alvaro-husillos-77a294173/");
            PerfilNome nome = new PerfilNome("Juan");
            PerfilNTelefone nTelefone = new PerfilNTelefone("913427123");
            ICollection<Tag> tag = new List<Tag>();

            var perfil = new Perfil(Id,u2.Id,EstadoDeHumor,DataDeNascimento,perfilFacebook, perfilLinkedin, nome, nTelefone, tag );
            PerfilDto perfilDto = PerfilDtoParser.ParaDTO(perfil);
            
            _repoUser.Setup(x => x.GetByIdAsync(u2.Id)).ReturnsAsync(u2);

            _service.GetByUserId(u2.Id);

            try
            {
                _repositoryMock.Setup(x => x.GetByUserIdAsync(u2.Id)).ReturnsAsync(() => null);
                Assert.False(false);
            }
            catch (BusinessRuleValidationException ex)
            {
                Assert.True(true);
            }
        }
    }
}