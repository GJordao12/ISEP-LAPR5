using System;
using System.Collections.Generic;
using DDDSample1.Domain.EstadosDeHumor;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.Perfis
{
    public class PerfilDto
    {
        public Guid Id { get; set; }
        
        public UserId userId { get;  set; }
        
        public EstadoDeHumorId estadoDeHumorId { get;  set; }
        public PerfilDataDeNascimento perfilDataDeNascimento { get;   set; }
        public PerfilFacebook perfilFacebook { get;   set; }
        public PerfilLinkedin perfilLinkedin { get;   set; }
        public PerfilNome perfilNome { get;   set; }
        public PerfilNTelefone perfilNTelefone { get;   set; }
        public virtual ICollection<TagDTO> ListaTags { get; set; }

        public PerfilDto(Guid id,UserId userId, EstadoDeHumorId estadoDeHumorId,PerfilDataDeNascimento perfilDataDeNascimento, PerfilFacebook perfilFacebook, PerfilLinkedin perfilLinkedin,PerfilNome perfilNome, PerfilNTelefone perfilNTelefone, ICollection<TagDTO> ListaTags)
        {
            Id = id;
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