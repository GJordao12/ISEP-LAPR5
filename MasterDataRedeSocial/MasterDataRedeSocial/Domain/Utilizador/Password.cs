using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DDDSample1.Domain.Utilizador
{
    public class Password : ValueObject
    {
        public string pwd { get;  private set; }

        public Password()
        {
            
        }
        public Password(string pwd)
        {
            this.pwd = pwd;
        }
        
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return pwd;
          
        }
    }
}