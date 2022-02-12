using System;
using System.Collections.Generic;
using DDDSample1.Domain.EstadosDeHumor;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.Perfis
{
    public class Perfil : Entity<PerfilId>, IAggregateRoot

    {
        public PerfilId Id { get;   set; }
        public UserId UserId { get;   set; }
        public EstadoDeHumorId estadoDeHumorId { get;   set; }
        public PerfilDataDeNascimento perfilDataDeNascimento { get;   set; }
        public PerfilFacebook perfilFacebook { get;   set; }
        public PerfilLinkedin perfilLinkedin { get;   set; }
        public PerfilNome perfilNome { get;   set; }
        public PerfilNTelefone perfilNTelefone { get;   set; }
        public virtual ICollection<Tag> ListaTags { get; set; }
        
        public double valorAngustiado { get; set; }
        public double valorMedroso { get; set; }
        public double valorDesapontado { get; set; }
        public double valorComRemorsos { get; set; }
        public double valorRaivoso{ get; set; }
        public double valorEsperancoso { get; set; }
        public double valorAliviado { get; set; }
        public double valorOrgulhoso { get; set; }
        public double valorGrato { get; set; }
        public double valorAlegria { get; set; }
        
        

        public Perfil(PerfilId id, UserId userId, EstadoDeHumorId estadoDeHumorId, 
            PerfilDataDeNascimento perfilDataDeNascimento, PerfilFacebook perfilFacebook, 
            PerfilLinkedin perfilLinkedin, PerfilNome perfilNome, PerfilNTelefone perfilNTelefone, 
            ICollection<Tag> ListaTags)
        {
            Id = id;
            UserId = userId;
            this.estadoDeHumorId = estadoDeHumorId;
            this.perfilDataDeNascimento = perfilDataDeNascimento;
            this.perfilFacebook = perfilFacebook;
            this.perfilLinkedin = perfilLinkedin;
            this.perfilNome = perfilNome;
            this.perfilNTelefone = perfilNTelefone;
            this.ListaTags = ListaTags;
            this.valorAngustiado = 0.5;
            this.valorAlegria = 0.5;
            this.valorAliviado = 0.5;
            this.valorDesapontado = 0.5;
            this.valorEsperancoso = 0.5;
            this.valorGrato = 0.5;
            this.valorMedroso = 0.5;
            this.valorRaivoso = 0.5;
            this.valorOrgulhoso = 0.5;
            this.valorComRemorsos = 0.5;

        }
        public Perfil(EstadoDeHumorId estadoDeHumorId,UserId userId,PerfilDataDeNascimento perfilDataDeNascimento,PerfilFacebook perfilFacebook,PerfilLinkedin perfilLinkedin,PerfilNome perfilNome, PerfilNTelefone perfilNTelefone)
        {
            this.Id = new PerfilId(Guid.NewGuid());
            this.UserId =userId;
            this.estadoDeHumorId = estadoDeHumorId;
            this.perfilDataDeNascimento = perfilDataDeNascimento;
            this.perfilFacebook = perfilFacebook;
            this.perfilLinkedin = perfilLinkedin;
            this.perfilNome = perfilNome;
            this.perfilNTelefone = perfilNTelefone;
        }

        public Perfil()
        {
            
        }
        
        public Perfil(Guid id, UserId userId)
        {
            this.Id = new PerfilId(id);
            this.UserId = userId;
        }

        public void ChangeEstadoDeHumorId(EstadoDeHumorId estadoDeHumorId)
        {
            this.estadoDeHumorId = estadoDeHumorId;
        }
        
        public void UpdateListaTags(ICollection<Tag> listaTags)
        {
            foreach (var tag in listaTags)
            {
                if (!this.ListaTags.Contains(tag))
                { 
                    this.ListaTags.Add(tag);    
                }
            }

            List<Tag> tagList = new List<Tag>(ListaTags);
            
            for (int i=0;i<tagList.Count;i++)
            {
                if (!listaTags.Contains(tagList[i]))
                {
                    tagList.Remove(tagList[i]);
                    i--;
                }
            }

            ICollection<Tag> tagsList = tagList;
            this.ListaTags = tagsList;
        }

        public void UpdateDados(string perfilDataDeNascimento,string perfilFacebook,string perfilLinkedin,string perfilNome, string perfilNTelefone)
        {
            this.perfilDataDeNascimento = new PerfilDataDeNascimento(perfilDataDeNascimento);
            this.perfilFacebook = new PerfilFacebook(perfilFacebook);
            this.perfilLinkedin = new PerfilLinkedin(perfilLinkedin);
            this.perfilNome = new PerfilNome(perfilNome);
            this.perfilNTelefone = new PerfilNTelefone(perfilNTelefone);
        }
        
    }
}