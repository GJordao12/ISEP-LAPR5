namespace DDDSample1.Domain.Utilizador
{
    public class CreatingUserDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public CreatingUserDTO(string email,string username, string  password)
        {
            Email = email;
            Username = username;
            Password = password;
        }
    }
}