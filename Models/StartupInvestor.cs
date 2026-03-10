using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class StartupInvestor
    {
        public int Startupid { get; set; }
        public string Userid { get; set; } = null!;
        public short Status { get; set; }
        public DateTime? DateTimeFav { get; set; }
    }
}
