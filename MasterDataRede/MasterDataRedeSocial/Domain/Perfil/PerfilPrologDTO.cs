
using System.Collections.Generic;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.Perfis
{
    public class PerfilPrologDTO
    {
        public string id { get;  set; }
        
        public string nome { get; set; }
        public virtual ICollection<string> ListaTags { get; set; }
        
        public double valorAngustiado { get; set; }
        public double valorMedroso { get; set; }
        public double valorDesapontado { get; set; }
        public double valorComRemorsos { get; set; }
        public double valorRaivoso { get; set; }
        public double valorEsperancoso { get; set; }
        public double valorAliviado { get; set; }
        public double valorOrgulhoso { get; set; }
        public double valorGrato { get; set; }
        public double valorAlegria { get; set; }

        public PerfilPrologDTO(UserPrologDTO user, ICollection<string> ListaTags,double valorDesapontado,double valorAlegria,double valorAliviado,double valorAngustiado,
            double valorEsperancoso,double valorGrato,double valorMedroso,double valorRaivoso,double valorOrgulhoso,double valorComRemorsos)
        {
            this.id = user.Id.ToString();
            this.nome = user.Username;
            this.valorAngustiado = valorAngustiado;
            this.valorAlegria = valorAlegria;
            this.valorAliviado = valorAliviado;
            this.valorDesapontado = valorDesapontado;
            this.valorEsperancoso = valorEsperancoso;
            this.valorGrato = valorGrato;
            this.valorMedroso = valorMedroso;
            this.valorRaivoso = valorRaivoso;
            this.valorOrgulhoso =valorOrgulhoso;
            this.valorComRemorsos = valorComRemorsos;
            
            this.ListaTags = ListaTags;
        }
    }
}