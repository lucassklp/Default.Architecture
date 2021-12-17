using Domain.Entities;
using Default.Architecture.CrossCutting.Extensions;
using Persistence.Repository;
using Default.Architecture.Services.Exceptions;
using Domain.Dtos;
using System.Threading.Tasks;

namespace Default.Architecture.Services
{
    public class UserServices
    {
        private UserRepository userRepository;
        public UserServices(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> RegisterAsync(RegisterUserDto userDto)
        {
            var isRegistred = await userRepository.IsRegistredAsync(userDto);

            if(!isRegistred)
            {
                var user = new User
                {
                    Email = userDto.Email,
                    Name = userDto.Name,
                    Password = userDto.Password.ToSha512()
                };

                var createdUser = await userRepository.CreateAsync(user);
                return createdUser;
            }

            throw new ExistentUserException(userDto);

        }
    }
}
