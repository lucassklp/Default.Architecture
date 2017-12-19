using DefaultArchitecture.Services.Exceptions;
using DefaultArchitecture.Services.Interfaces;
using Domain;
using Repository.Interfaces;
using Extensions;
using MySql.Data.MySqlClient;

namespace DefaultArchitecture.Services
{
    public class UserServices : IUserServices
    {
        IUserRepository userRepository;
        public UserServices(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User Register(User user)
        {
            try
            {
                user.Password = user.Password.ToSHA512();
                return userRepository.Register(user);
            }
            catch(MySqlException ex)
            {
                if(ex.Number == (int)MySqlErrors.DuplicatedKey)
                {
                    throw new UserExistentException(user);
                }
            }
            return user;
        }

    }
}
