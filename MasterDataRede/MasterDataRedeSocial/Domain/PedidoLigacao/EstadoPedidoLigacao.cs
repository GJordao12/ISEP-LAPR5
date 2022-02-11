using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.PedidoLigacao
{
    public class Estado : ValueObject
    {
        public string estado { get; set; }
        public Estado(String estado)
        {
            if (isValid(estado))
            {
                this.estado = estado;   
            }
            else
            {
                throw new BusinessRuleValidationException("Estado Inválido.");
            }
        }
        public Estado()
        {
        }

        public Estado Aprovar()
        {
            this.estado = "Aceite";
            return this;
        }
        
        public Estado Desaprovar()
        {
            this.estado = "Rejeitado";
            return this;
        } 
        
        public Estado Pendente()
        {
            this.estado = "Pendente";
            return this;
        }

        public bool isValid(string estado)
        {
            if (estado.Equals("Aceite") || estado.Equals("Rejeitado") || estado.Equals("Pendente"))
            {
                return true;
            }
            return false;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return estado;
          
        }
    }
}