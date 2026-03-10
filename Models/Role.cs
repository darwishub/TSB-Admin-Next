using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<AdminUser1>();
        }

        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        public virtual ICollection<AdminUser1> Users { get; set; }
    }
}
