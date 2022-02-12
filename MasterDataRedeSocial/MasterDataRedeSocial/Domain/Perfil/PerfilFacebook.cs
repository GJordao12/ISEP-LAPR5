using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Perfis
{
    public class PerfilFacebook: ValueObject
    {
        public PerfilFacebook()
        {
            
        }
        public string LinkFacebook { get;  private set; }
        
        public PerfilFacebook(string linkFacebook)
        {
            
            this.LinkFacebook = linkFacebook;
            
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return LinkFacebook;
        }
    }
}