using System;

namespace Business.Exceptions
{
    public class DefaultException : Exception
    {
        public int ErrorCode { get; set; }
        public DefaultException((int, string) systemError) : base(systemError.Item2)
        {
            this.ErrorCode = systemError.Item1;
        }

        public DefaultException((int, string) systemError, string customMessage) : base(customMessage)
        {
            this.ErrorCode = systemError.Item1;
        }
    }
}