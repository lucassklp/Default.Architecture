using Domain.Dtos;
using Domain.Entities;

namespace Default.Architecture.Business.Exceptions
{
    public class ExistentUserException : BusinessException
    {
        public ExistentUserException(RegisterUserDto user)
            : base($"The user with email {user.Email} already exists", "existent-user")
        {

        }
    }
}
