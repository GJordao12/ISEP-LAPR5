using System;

namespace DDDSample1.Domain.Utilizador
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        
        public string Password { get; set; }

        public UserDTO()
        {
            
        }
        
        public UserDTO(string email,string username /*, string password*/)
        {
            this.Id = Guid.NewGuid();
            this.Email = email;
            this.Username = username;
            //this.Password = password;
        }
        
        
        public UserDTO(Guid id,string email,string username, string password)
        {
            this.Id = id;
            this.Email = email;
            this.Username = username;
            this.Password = password;
        }
        public UserDTO(Guid id,string email,string username)
        {
            this.Id = id;
            this.Email = email;
            this.Username = username;
        }
    }
}