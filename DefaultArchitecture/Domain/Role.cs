using System.Collections.Generic;

namespace DefaultArchitecture.Domain
{
    public class Role : Identifiable
    {
        public long ID { get; set; }
        public string Description { get; set; }

        public virtual IList<User> Users { get; set; }
    }
}