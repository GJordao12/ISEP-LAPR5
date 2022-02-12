using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Perfis
{
    
    
    public class PerfilDataDeNascimento: ValueObject
    {
       
        public string Data { get;  private set; } 
        
        public PerfilDataDeNascimento()
        {
            
        }
        
        public PerfilDataDeNascimento(string data)
        {
        
            this.Data = data;
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
            yield return Data;
        }
    }
}