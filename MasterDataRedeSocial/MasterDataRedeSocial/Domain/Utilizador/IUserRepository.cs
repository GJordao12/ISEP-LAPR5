using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Utilizador
{
    public interface IUserRepository : IRepository<User,UserId>
    {
        Task<User> GetUserByUsername(string username);

        Task<List<User>> GetUsersWithSearchNameFieldAsync(UserId id,string nome);
        
        Task<List<User>>GetUsersWithSearchEmailFieldAsync(UserId id,string email);
    }
}