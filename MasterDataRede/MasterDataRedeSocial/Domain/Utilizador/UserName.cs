using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DDDSample1.Domain.Utilizador
{
    public class UserName : ValueObject
    {
        public string userName { get;  set; }

        public UserName()
        {
            
        }
        
        public UserName(string userName)
        {
            
            
            this.userName = userName;
        }

        public bool isValid(string userName)
        {
            if (Regex.IsMatch(userName, "^[a-zA-Z[0-9]?]*$"))
            {
                return true;
            }

            return false;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return userName;
          
        }

        public override string ToString()
        {
            return userName;
        }
    }
}