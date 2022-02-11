using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Utilizador;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure.Utilizador
{
    public class UserRepository : BaseRepository<User, UserId>, IUserRepository
    {
        public UserRepository(DDDSample1DbContext context) : base(context.User)
        {

        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await this._objs.Where(r => r.Username.userName.Equals(username)).FirstOrDefaultAsync();
        }
        public async Task<List<User>> GetUsersWithSearchNameFieldAsync(UserId id,string nome)
        {
            return await _objs.Where(u=>!u.Id.Equals(id) && u.Username.userName.Equals(nome)).ToListAsync();
        }
        
        public async Task<List<User>> GetUsersWithSearchEmailFieldAsync(UserId id,string email)
        {
            return await _objs.Where(u=>!u.Id.Equals(id) && u.Email.EmailLogin.Equals(email)).ToListAsync();
        }
    }
}