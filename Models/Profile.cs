using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class Profile
    {
        public string Userid { get; set; } = null!;
        public int Questionid { get; set; }
        public string Value { get; set; } = null!;
    }
}
