using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Utilizador
{
    public class User : Entity<UserId>, IAggregateRoot
    {
        public virtual UserId Id { get; set; }
        public virtual Email Email{ get; set; }

        public virtual UserName Username { get; set; }
        
        public virtual Password Password { get; set; }
        
        public User()
        {
            
        }
        
        public User(UserId id,Email email, UserName username, Password password)
        {
            this.Id = id;
            this.Email = email;
            this.Username = username;
            this.Password = password;
        }

        public User(string id,string email, string username, string password)
        {
            this.Id = new UserId(id);
            this.Email = new Email(email);
            this.Username = new UserName(username);
            this.Password = new Password(password);
        }
        
        public User(UserId id,Email email, UserName username)
        {
            this.Id = id;
            this.Email = email;
            this.Username = username;
        }
        
    }
}