using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DDDSample1.Domain.PedidoIntroducao
{
    public class Apresentacao : ValueObject
    {   
        
        public string apresentacaoPedido { get; set; }

        public Apresentacao()
        {

        }

        public Apresentacao(string apresentacaoPedido)
        {
            /*if (isValid(apresentacaoPedido))
            {
                this.apresentacaoPedido = apresentacaoPedido; 
            }

            else
            {
                throw new ArgumentException("A apresentacao que tentou colocar não é válido para o nosso sistema.");
            }*/
            
            this.apresentacaoPedido = apresentacaoPedido;
        }

        public bool isValid(string apresentacao)
        {
            if (Regex.IsMatch(apresentacao, "([a-zA-Z0-9',.!?=*^@~-]+( [a-zA-Z0-9',.!?=@*^~-]+)*){1,200}"))
            {
                return true;
            }

            return false;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return apresentacaoPedido;
          
        }
    }
}