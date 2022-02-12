using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Perfis
{
    
    public class PerfilNTelefone: ValueObject
    {
        public PerfilNTelefone()
        {
            
        }
        
        public string NTelefono { get;  private set; }
        
        public PerfilNTelefone( string ntelefono)
        {


            this.NTelefono = ntelefono;

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
            yield return NTelefono;
        }
    }
}