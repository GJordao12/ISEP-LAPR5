using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Perfis
{
    public class PerfilNome: ValueObject
    {
        public PerfilNome()
        {
            
        }
        public string NomePerfil { get;  private set; } 
        public PerfilNome(string nomePerfil)
        {
            
            this.NomePerfil = nomePerfil;
            
        }
        public bool isValid(string estado)
        {
            if (estado.Equals("APROVADO") || estado.Equals("DESAPROVADO") || estado.Equals("PENDENTE"))
            {
                return true;
            }

            return false;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return NomePerfil;
        }
    }
}