using System.Collections.Generic;

namespace Domain
{
    public class User : Identifiable
    {
        public long ID { get; set; }
        public string Email { get; set; }
        public string Name { get; private set; }
        public string Password { get; set; }

        public virtual IList<Role> Roles { get; set; }
    }
}
