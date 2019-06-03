using Domain.Entities;

namespace Business.Exceptions
{
    public class UserExistentException : BusinessException
    {
        public UserExistentException(User user)
            : base($"The user with email {user.Email} already exists")
        {

        }
    }
}
