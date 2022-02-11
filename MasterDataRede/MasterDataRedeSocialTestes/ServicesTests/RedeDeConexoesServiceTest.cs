using System;
using System.Collections.Generic;
using DDDSample1.Domain.EstadosDeHumor;
using DDDSample1.Domain.Ligacoes;
using DDDSample1.Domain.Perfis;
using DDDSample1.Domain.RedesConexoes;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;
using Moq;
using Xunit;

namespace MasterDataRedeSocialTestes
{
    public class RedeDeConexoesServiceTest
    {
        private readonly RedeDeConexoesService _service;
        private readonly Mock<IPerfilRepository> _repoPerfil = new Mock<IPerfilRepository>();
        private readonly Mock<ILigacaoRepository> _repoLigacao = new Mock<ILigacaoRepository>();
        private readonly Mock<IUserRepository> _repoUser = new Mock<IUserRepository>();

        public RedeDeConexoesServiceTest()
        {
            _service = new RedeDeConexoesService(_repoLigacao.Object, _repoUser.Object,_repoPerfil.Object);
        }

        [Fact]
        public void GetByIdAndLevelAsyncTestNull()
        {
            User user = new User(new UserId(new Guid()), new Email("utilizador@hotmail.com"), new UserName("userName10"), new Password("&Pwd123456789"));
            _repoUser.Setup(x => x.GetByIdAsync(user.Id)).ReturnsAsync(() => null);

            try
            {
                _service.GetByIdAndLevelAsync(user.Id, 1);
                Assert.False(false);
            }
            catch (BusinessRuleValidationException ex)
            {
                Assert.True(true);
            }
        }
        
        [Fact]
        public void GetByIdAndLevelAsyncTest()
        {
            User user1 = new User(new UserId(new Guid()), new Email("utilizador1@hotmail.com"), new UserName("userName1"), new Password("&Pwd123456789"));
            User user2 = new User(new UserId(new Guid()), new Email("utilizador2@hotmail.com"), new UserName("userName2"), new Password("&Pwd123456789"));
            _repoUser.Setup(x => x.GetByIdAsync(user1.Id)).ReturnsAsync(() => user1);
            _repoUser.Setup(x => x.GetByIdAsync(user2.Id)).ReturnsAsync(() => user2);

            
            PerfilId Id = new PerfilId(Guid.NewGuid());
            UserId userId = user1.Id;
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
            _repoPerfil.Setup(x => x.GetByUserIdAsync(user1.Id)).ReturnsAsync(perfil);

            Ligacao lig = new Ligacao(new LigacaoID("0089f1d9-4d80-4bb3-9648-aa9fea779e8f"),user2.Id, user1.Id, new ForçaLigacao(2), new ForçaRelacao(2), listTags);
            List<Ligacao> listLig = new List<Ligacao>();
            listLig.Add(lig);
            Dictionary<int, List<Ligacao>> dic = new Dictionary<int, List<Ligacao>>();
            dic.Add(1,listLig);
            _repoLigacao.Setup(x => x.GetByIdAndLevelAsync(user1.Id,1)).ReturnsAsync(dic);
            
            Dictionary<int, List<LigacaoRedeDeConexoesDTO>> dicDto = new Dictionary<int, List<LigacaoRedeDeConexoesDTO>>();
            LigacaoRedeDeConexoesDTO lrcDto = new LigacaoRedeDeConexoesDTO(lig.Id, user2.Id,
                UserDTOParser.ParaDTOSemPass(user1), listTagDTO, lig.ForcaLigacao.forcaLigacao,
                lig.ForçaRelacao.forcaRelacao, listTagDTO);
            List<LigacaoRedeDeConexoesDTO> listaLRCDto = new List<LigacaoRedeDeConexoesDTO>();
            listaLRCDto.Add(lrcDto);
            dicDto.Add(1,listaLRCDto);
            
            Assert.Equal(dicDto.ToString(),_service.GetByIdAndLevelAsync(user2.Id,1).Result.ToString());
        }
    }
}