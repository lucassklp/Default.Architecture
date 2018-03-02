using System.Collections.Generic;

namespace Domain.Entities
{
    public class User : Identifiable<long>
    {
        public long ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
