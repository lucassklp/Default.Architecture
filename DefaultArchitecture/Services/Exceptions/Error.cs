using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultArchitecture.Services.Exceptions
{
    public class Error
    {
        public String Message { get; set; }
        public int ErrorCode { get; set; }

        public Error(DefaultException exception)
        {
            this.Message = exception.Message;
            this.ErrorCode = (int)exception.ErrorCode;
        }
    }
}
