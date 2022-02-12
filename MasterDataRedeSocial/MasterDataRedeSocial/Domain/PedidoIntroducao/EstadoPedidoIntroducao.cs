using System;
using System.Collections.Generic;

namespace DDDSample1.Domain.PedidoIntroducao
{
    public class EstadoPedidoIntroducao : ValueObject
    {
        public string estadoPedidoIntroducao { get; set; }
        
        public EstadoPedidoIntroducao()
        {
            
        }
        public EstadoPedidoIntroducao(string estado)
        {
            if (isValid(estado))
            {
                this.estadoPedidoIntroducao = estado;
            }
            else
            {
                throw new ArgumentException(
                    "O estado que pretende colocar, não se encontra nas opções. Insira um válido.");
            }
            
        }

        public EstadoPedidoIntroducao Aprovar()
        {
            this.estadoPedidoIntroducao = "APROVADO";
            return this;
        }
        
        public EstadoPedidoIntroducao Desaprovar()
        {
            this.estadoPedidoIntroducao = "DESAPROVADO";
            return this;
        } 
        
        public EstadoPedidoIntroducao Pendente()
        {
            this.estadoPedidoIntroducao = "PENDENTE";
            return this;
        }
        
        /*
        private sealed class EstadoPedidoIntroducaoEqualityComparer : IEqualityComparer<EstadoPedidoIntroducao>
        {
            public bool Equals(EstadoPedidoIntroducao x, EstadoPedidoIntroducao y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.estadoPedidoIntroducao == y.estadoPedidoIntroducao;
            }

            public int GetHashCode(EstadoPedidoIntroducao obj)
            {
                return (obj.estadoPedidoIntroducao != null ? obj.estadoPedidoIntroducao.GetHashCode() : 0);
            }
        }

        public static IEqualityComparer<EstadoPedidoIntroducao> EstadoPedidoIntroducaoComparer { get; } = new EstadoPedidoIntroducaoEqualityComparer();
        
        */

        public bool isValid(string estado)
        {
            if (estado.Equals("APROVADO") || estado.Equals("DESAPROVADO") || estado.Equals("PENDENTE"))
            {
                return true;
            }

            return false;
        }
        public override string ToString()
        {
            return estadoPedidoIntroducao;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return estadoPedidoIntroducao;
          
        }
    }
}