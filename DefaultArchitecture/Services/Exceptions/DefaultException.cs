using System;

namespace DefaultArchitecture.Services.Exceptions
{
    public class DefaultException : Exception
    {
        public SystemErrorCode ErrorCode { get; set; }

        public DefaultException(String message, SystemErrorCode codeError) : base(message)
        {
            this.ErrorCode = codeError;
        }
    }
}