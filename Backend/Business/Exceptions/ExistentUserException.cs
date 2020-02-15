using Domain.Entities;

namespace Business.Exceptions
{
    public class ExistentUserException : BusinessException
    {
        public ExistentUserException(User user)
            : base($"The user with email {user.Email} already exists", "existent-user")
        {

        }
    }
}
