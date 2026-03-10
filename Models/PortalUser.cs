using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class PortalUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string? Password { get; set; }
        public int RoleId { get; set; }
    }
}
