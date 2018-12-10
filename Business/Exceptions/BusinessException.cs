using System;

namespace Business.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string msg) :
            base(msg)
        { }
    }
}
