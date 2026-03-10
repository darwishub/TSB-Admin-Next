using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class NetworkingProfile
    {
        public string Userid { get; set; } = null!;
        public string MyRole { get; set; } = null!;
        public string MyExpertise { get; set; } = null!;
        public string ProfileImgUrl { get; set; } = null!;
        public string NetworkWithRoles { get; set; } = null!;
        public string NetworkWithExpertiseAt { get; set; } = null!;
        public bool? Ispublic { get; set; }
    }
}
