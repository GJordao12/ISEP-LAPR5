using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Ligacoes;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;
using FluentAssertions;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace MasterDataRedeSocialTestes
{
    public class LigacoesServiceTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly LigacaoService _service;
        private readonly TagService _serviceTag;
        private readonly Mock<ILigacaoRepository> _repositoryMock = new Mock<ILigacaoRepository>();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<ITagRepository> _repositoryTagMock = new Mock<ITagRepository>();
        private readonly Mock<IUserRepository> _repositoryUserMock = new Mock<IUserRepository>();

        private Tag tag1;
        private Tag tag2;
        private TagDTO tagDto1;
        private TagDTO tagDto2;
        private ICollection<Tag> listaTags = new List<Tag>();
        private List<TagDTO> listaTagsDTO = new List<TagDTO>();
        private UserId userPrincipalId;
        private UserId userSecundarioId;
        private ForçaLigacao fl;
        private ForçaRelacao fr;
        private Ligacao ligacao;
        private LigacaoDTO ligacaoDto;
        private List<Ligacao> ligacaos = new List<Ligacao>();
        private List<LigacaoDTO> ligacaosDto = new List<LigacaoDTO>();
        private LigacaoPutDTO ligacaoPutDto;
        private CreatingLigacaoDTO creatingLigacaoDto;

        public LigacoesServiceTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _service = new LigacaoService(_unitOfWorkMock.Object, _repositoryMock.Object,_repositoryTagMock.Object,_repositoryUserMock.Object);
            
            tag1 = new Tag(new TagID(Guid.NewGuid()), new Nome("futebol"));
            tag2 = new Tag(new TagID(Guid.NewGuid()), new Nome("andebol"));

            tagDto1 = TagDTOParser.ParaDTO(tag1);
            tagDto2 = TagDTOParser.ParaDTO(tag2);
            
            listaTags.Add(tag1);
            listaTags.Add(tag2);
            
            listaTagsDTO.Add(tagDto1);
            listaTagsDTO.Add(tagDto2);
            
            userPrincipalId = new UserId("5a2f5df6-68cb-405d-9f3b-0e94b53784d9");
            userSecundarioId = new UserId("5d2f5df6-68cb-405d-9f3b-0e94b53784e9");

            fl = new ForçaLigacao(2);
            fr = new ForçaRelacao(0);

            ligacao = new Ligacao(new LigacaoID("5b2f5df6-68cb-405d-9f3b-0e94b53784f9"),userPrincipalId, userSecundarioId, fl, fr, listaTags);

            ligacaoDto = LigacaoDTOParser.ParaDTO(ligacao);
            
            ligacaos.Add(ligacao);
            ligacaosDto.Add(ligacaoDto);

            ligacaoPutDto = new LigacaoPutDTO(fl, listaTagsDTO);
            creatingLigacaoDto = new CreatingLigacaoDTO(userPrincipalId, userSecundarioId, 2, 0, listaTagsDTO);
        }

        [Fact]
        public void GetAllAsyncTest()
        {
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(ligacaos);
            Task<List<LigacaoDTO>> result = _service.GetAllAsync();
            
            ligacaosDto.Should().BeEquivalentTo(result.Result);
        }

        [Fact]
        public void GetByIdTest()
        {
            _repositoryMock.Setup(x => x.GetByIdAsync(new LigacaoID("5b2f5df6-68cb-405d-9f3b-0e94b53784f9"))).ReturnsAsync(ligacao);
            var result = _service.GetById(new LigacaoID("5b2f5df6-68cb-405d-9f3b-0e94b53784f9"));
            
            ligacaoDto.Should().BeEquivalentTo(result.Result.Value);
        }
        
        [Fact]
        public void GetByIdTestNull()
        {
            _repositoryMock.Setup(x => x.GetByIdAsync(new LigacaoID("5b2f5df6-68cb-405d-9f3b-0e94b53784a9")))
                .ReturnsAsync(() => null);

            var result = _service.GetById(new LigacaoID(new Guid()));
            
            Assert.Null(result.Result);
        }
        
        [Fact]
        public void UpdateAsyncNull()
        {
            _repositoryMock.Setup(x => x.GetByIdAsync(new LigacaoID("5b2f5df6-68cb-405d-9f3b-0e94b53784f9")))
                .ReturnsAsync(() => null);
            
            var result = _service.UpdateAsync(new Guid("5b2f5df6-68cb-405d-9f3b-0e94b53784f9"), ligacaoPutDto);
            
            Assert.Null(result.Result);
        }

        [Fact]
        public void AddAsync()
        {
            var result = _service.AddAsync(creatingLigacaoDto);
            
            LigacaoDTO dtoResult = new LigacaoDTO(result.Result.Id, result.Result.principal, result.Result.secundario,
                result.Result.forcaLigacao, result.Result.forçaRelacao, result.Result.listaTags);
            
            _repositoryMock.Setup(x => x.GetByIdAsync(new LigacaoID(dtoResult.Id)))
                .ReturnsAsync(LigacaoDTOParser.DeDTO(dtoResult));
            
            dtoResult.Should().BeEquivalentTo(result.Result);
        }

        [Fact]
        public void GetLigacoesByUser()
        {
            _repositoryMock.Setup(x => x.GetLigacoesbyUser(new UserId("5b2f5df6-68cb-405d-9f3b-0e94b53784f9")))
                .ReturnsAsync(ligacaos);

            var result = _service.GetLigacoesByUser(new UserId("5b2f5df6-68cb-405d-9f3b-0e94b53784f9"));

            ligacaosDto.Should().BeEquivalentTo(result.Result);

        }

        [Fact]
        public void UpdateAsync()
        {
            var listTag = new List<Tag>();
            listTag.Add(tag1);

            var listTag2 = new List<Tag>();
            listTag2.Add(tag2);
            
            _repositoryMock.Setup(x => x.GetByIdAsync(ligacao.Id)).ReturnsAsync(ligacao);
            
            _repositoryTagMock.Setup(x => x.GetByNomeAsync("futebol")).ReturnsAsync(listTag);
            _repositoryTagMock.Setup(x => x.GetByNomeAsync("andebol")).ReturnsAsync(listTag2);
            
            var result = _service.UpdateAsync(ligacao.Id.AsGuid(), ligacaoPutDto);
            
            ligacaoDto.Should().BeEquivalentTo(result.Result);
        }
        
        [Fact]
        public void UpdateAsyncCount0()
        {
            var listTag = new List<Tag>();
            listTag.Add(tag1);

            var listTag2 = new List<Tag>();

            _repositoryMock.Setup(x => x.GetByIdAsync(ligacao.Id)).ReturnsAsync(ligacao);
            
            _repositoryTagMock.Setup(x => x.GetByNomeAsync(tag1.nome.nome)).ReturnsAsync(listTag);
            _repositoryTagMock.Setup(x => x.GetByNomeAsync(tag2.nome.nome)).ReturnsAsync(listTag2);
            _repositoryTagMock.Setup(x => x.AddAsync(tag2));

            var result = _service.UpdateAsync(ligacao.Id.AsGuid(), ligacaoPutDto);
            
            Assert.Equal(ligacaoDto.listaTags.Count.ToString(),result.Result.listaTags.Count.ToString());
        }
    }
}