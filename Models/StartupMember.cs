using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class StartupMember
    {
        public int Startupid { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public short Status { get; set; }
        public short Type { get; set; }
    }

    public partial class UserMembers
    {
        public int Startupid { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public short Status { get; set; }
        public short Type { get; set; }
        public string? UserId { get; set; }
    }

    public partial class MembersOfStartup
    {
        public int Startupid { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public short Status { get; set; }
        public short Type { get; set; }
        public string? UserId { get; set; }
        public string? Photo { get; set; }
        public bool isOwner { get; set; }
    }
}
