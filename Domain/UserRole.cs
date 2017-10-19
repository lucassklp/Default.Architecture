using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class UserRole
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long RoleId { get; set; }
        public Role Role { get; set; }
    }
}
