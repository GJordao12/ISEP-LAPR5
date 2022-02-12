using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DDDSample1.Domain.Utilizador
{ 
    public class Email : ValueObject
    {
        public string EmailLogin { get; set; }
        public Email()
        {
            
        }
        public Email(string value)
        {
           if (IsValid(value))
           {
               EmailLogin = value;
           }
           else
           {
               throw new ArgumentException("O email que está a inserir é inválido.");
           }
           
        }
        public bool IsValid(string emailAddress)
        {
            return Regex.IsMatch(emailAddress,"^[a-zA-Z0-9][a-zA-Z0-9._-]*@[a-zA-Z0-9][a-zA-Z0-9._-]*\\.[a-zA-Z]{2,4}$");
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return EmailLogin;
          
        }
        
    }
}