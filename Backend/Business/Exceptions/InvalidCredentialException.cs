namespace Business.Exceptions
{
    public class InvalidCredentialException : BusinessException
    {
        public InvalidCredentialException()
            : base("The credentials are incorrect")
        {

        }
    }
}
