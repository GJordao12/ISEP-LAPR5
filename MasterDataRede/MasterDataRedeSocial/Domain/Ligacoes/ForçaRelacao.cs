using System;
using System.Collections.Generic;

namespace DDDSample1.Domain.Ligacoes
{
    public class ForçaRelacao : ValueObject
    {
        public int forcaRelacao { get; set; }

        public ForçaRelacao()
        {
            
        }

        public ForçaRelacao(int forcaRelacao)
        {
            if (isValid(forcaRelacao))
            {
                this.forcaRelacao = forcaRelacao;    
            }
            else
            {
                throw new ArgumentException("A força de relação não tem valor compreendido entre 0 e 100");
            }
            
        }

        public bool isValid(int forcaRelacao)
        {
            if (forcaRelacao >= 0 && forcaRelacao < 100)
            {
                return true;
            }

            return false;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return forcaRelacao;
        }
    }
}