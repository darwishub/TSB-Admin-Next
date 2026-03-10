using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class StartupBmc
    {
        public int Startupid { get; set; }
        public string Data { get; set; } = null!;
        public DateTime Lastupdated { get; set; }
    }
}
