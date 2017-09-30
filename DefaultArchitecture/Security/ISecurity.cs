using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultArchitecture.Security
{
    public interface ISecurity<T> where T: class
    {
        string Login(T identity);
        string Logout(T identity);
    }
}
