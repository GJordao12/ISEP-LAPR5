using System;

namespace DDDSample1.Domain.Utilizador
{
    public class UserDTOParser
    {
        public static UserDTO ParaDTO(User user)
        {
            return new UserDTO(user.Id.AsGuid(),user.Email.EmailLogin,user.Username.userName,user.Password.pwd);
        }

        public static UserDTO ParaDTOSemPass(User user)
        {
            return new UserDTO(user.Id.AsGuid(),user.Email.EmailLogin,user.Username.userName);
        }
        
        public static User DeDTO(UserDTO dto)
        {
            return new User(new UserId(Guid.NewGuid()),new Email(dto.Email),new UserName(dto.Username), new Password(dto.Password));
        }

        public static User DeDTOComID(UserDTO dto)
        {
            return new User(new UserId(dto.Id), new Email(dto.Email), new UserName(dto.Username),
                new Password(dto.Password));
        }

        public static User DeDTOSemID(CreatingUserDTO dto)
        {
            return new User(new UserId(Guid.NewGuid()), new Email(dto.Email), new UserName(dto.Username),
                new Password(dto.Password));
        }
    }
}