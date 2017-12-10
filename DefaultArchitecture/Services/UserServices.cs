using DefaultArchitecture.Services.Exceptions;
using DefaultArchitecture.Services.Interfaces;
using Domain;
using Repository.Interfaces;
using Extensions;

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
            if (!userRepository.Exists(user))
            {
                //Criptography the password
                user.Password = user.Password.ToSHA512();
                return userRepository.Register(user);
            }
            else
            {
                throw new UserExistentException(user);
            }
        }

    }
}
