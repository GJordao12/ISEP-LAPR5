using System.Collections.Generic;
using DDDSample1.Domain.EstadosDeHumor;
using DDDSample1.Domain.Tags;
using Newtonsoft.Json;

namespace DDDSample1.Domain.Perfis
{
    public class CreatingPerfilPutDto
    {
        public EstadoDeHumorDto estadoDeHumor { get;   set; }
        public string perfilDataDeNascimento { get;   set; }
        public string perfilFacebook { get;   set; }
        public string perfilLinkedin { get;   set; }
        public string perfilNome { get;   set; }
        public string perfilNTelefone { get;   set; }
        public ICollection<TagDTO> listaTags { get; set; }
        
        public CreatingPerfilPutDto(EstadoDeHumorDto estadoDeHumor, string perfilDataDeNascimento,
            string perfilFacebook, string perfilLinkedin, string perfilNome, string perfilNTelefone,
            ICollection<TagDTO> listaTags)
        {
            this.estadoDeHumor = estadoDeHumor;
            this.perfilDataDeNascimento = perfilDataDeNascimento;
            this.perfilFacebook = perfilFacebook;
            this.perfilLinkedin = perfilLinkedin;
            this.perfilNome = perfilNome;
            this.perfilNTelefone = perfilNTelefone;
            this.listaTags = listaTags;
        }

        public CreatingPerfilPutDto()
        {
            
        }
    }
}