using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Perfis
{
    public class PerfilLinkedin: ValueObject
    {
        public PerfilLinkedin()
        {
            
        }
        public string LinkLinkedin { get;  private set; }
        
        public PerfilLinkedin(string linkLinkedin)
        {
            
            this.LinkLinkedin = linkLinkedin;
            
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
             yield return LinkLinkedin;
        }
    }
}