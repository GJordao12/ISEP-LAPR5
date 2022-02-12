using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DDDSample1.Domain.PedidoLigacao
{
    public class Texto : ValueObject
    {
        public string texto { get; set; }

        public Texto()
        {
            
        }
        public Texto(string texto)
        {
            /*if (isValid(texto))
            {
                this.texto = texto;   
            }

            throw new ArgumentException("O texto que tentou colocar não é válido para o nosso sistema.");*/
            this.texto = texto;
        }
        public bool isValid(string texto)
        {
            if (Regex.IsMatch(texto, "([a-zA-Z0-9',.!?=*^@~-]+( [a-zA-Z0-9',.!?=@*^~-]+)*){1,200}"))
            {
                return true;    
            }
            
            return false;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return texto;
          
        }
    }
        
}