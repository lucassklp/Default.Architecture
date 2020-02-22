using System;

namespace Default.Architecture.Business.Exceptions
{
    public class InvalidCredentialException : BusinessException
    {
        public InvalidCredentialException(Exception ex)
            : base("The credentials are incorrect", "incorrect-credentials", ex)
        {
        }
    }
}
