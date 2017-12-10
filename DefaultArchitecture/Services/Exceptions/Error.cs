using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultArchitecture.Services.Exceptions
{
    public class Error<T> where T : Exception
    {
        public String Title { get; set; }
        public String Message { get; set; }

        public Error(T exception)
        {
            this.Title = typeof(T).Name;
            this.Message = exception.Message;
        }
    }
}
