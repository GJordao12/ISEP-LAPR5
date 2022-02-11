using System;

namespace DDDSample1.Domain.Utilizador
{
    public class UserPrologDTO
    {
            public Guid Id { get; set; }
            public string Username { get; set; }

            public UserPrologDTO(Guid id,string username)
            {
                this.Id = id;
                Username = username;
            }
        }
    }
    