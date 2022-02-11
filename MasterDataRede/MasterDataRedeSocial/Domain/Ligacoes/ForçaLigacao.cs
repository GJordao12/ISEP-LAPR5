using System;
using System.Collections.Generic;

namespace DDDSample1.Domain.Ligacoes
{
    public class ForçaLigacao : ValueObject
    {
        public int forcaLigacao { get; set; }

        public ForçaLigacao()
        {
            
        }

        public ForçaLigacao(int forcaLigacao)
        {
            if (isValid(forcaLigacao))
            {
                this.forcaLigacao = forcaLigacao;    
            }
            else
            {
                throw new ArgumentException("A força de ligação não tem valor compreendido entre 0 e 100");
            }
            
        }

        public bool isValid(int forcaLigacao)
        {
            if (forcaLigacao >= 0 && forcaLigacao < 100)
            {
                return true;
            }

            return false;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return forcaLigacao;
        }
    }
}