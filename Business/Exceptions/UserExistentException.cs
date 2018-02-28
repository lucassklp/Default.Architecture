using Domain.Entities;

namespace Business.Exceptions
{
    public class UserExistentException : DefaultException
    {
        public UserExistentException(User user) : base(SystemErrors.UserAlreadyExists, $"The user with email {user.Email} already exists")
        {

        }
    }
}
