using System.Collections.Generic;

namespace Domain.Entities
{
    public class Role : Identifiable<long>
    {
        public long Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}