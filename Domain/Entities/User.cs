using System.Collections.Generic;

namespace Domain.Entities
{
    public class User : Identifiable<long>, ICredential
    {
        public long ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        #region ICredential Members
        public string Password { get; set; }
        public string Login => Email;
        #endregion
    }
}
