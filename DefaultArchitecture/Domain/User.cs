using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultArchitecture.Domain
{
    public class User : Identifiable
    {
        public long ID { get; set; }
        public string Name { get; private set; }
        public string Password { get; set; }
    }
}
