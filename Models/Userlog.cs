using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class Userlog
    {
        public int Logid { get; set; }
        public string Userid { get; set; } = null!;
        public string Action { get; set; } = null!;
        public DateTime Datetime { get; set; }
    }
}
