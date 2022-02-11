using System;
using System.Collections.Generic;

namespace DDDSample1.Domain.Tags
{
    public class Nome : ValueObject
    {
        public string nome { get; set; }

        public Nome()
        {
            
        }

        public Nome(string nome)
        {
            if (isValid(nome))
            {
                this.nome = nome;    
            }
            else
            {
                throw new ArgumentException("O nome não é válido");
            }
            
        }

        public bool isValid(string nome)
        {
            if (nome.Length < 255)
            {
                return true;
            }

            return false;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return nome;
        }
    }
}