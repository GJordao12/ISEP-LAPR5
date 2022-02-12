using System.Collections.Generic;
using DDDSample1.Domain.EstadosDeHumor;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.Perfis

{
    public class CreatingPerfilDTO
    {
        public UserId userId { get;   set; }
        public EstadoDeHumorId estadoDeHumorId { get;   set; }
        public string perfilDataDeNascimento { get;   set; }
        public string perfilFacebook { get;   set; }
        public string perfilLinkedin { get;   set; }
        public string perfilNome { get;   set; }
        public string perfilNTelefone { get;   set; }
        public virtual ICollection<Tag> ListaTags { get; set; }
        public CreatingPerfilDTO(UserId userId, EstadoDeHumorId estadoDeHumorId,  string perfilDataDeNascimento, string perfilFacebook, string perfilLinkedin, string perfilNome, string perfilNTelefone, ICollection<Tag> ListaTags)
        {
            this.userId = userId;
            this.estadoDeHumorId = estadoDeHumorId;
            this.perfilDataDeNascimento = perfilDataDeNascimento;
            this.perfilFacebook = perfilFacebook;
            this.perfilLinkedin = perfilLinkedin;
            this.perfilNome = perfilNome;
            this.perfilNTelefone = perfilNTelefone;
            this.ListaTags = ListaTags;
        }
    }
}