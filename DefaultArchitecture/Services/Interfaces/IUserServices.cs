using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultArchitecture.Services.Interfaces
{
    public interface IUserServices
    {
        User Register(User user);
    }
}
