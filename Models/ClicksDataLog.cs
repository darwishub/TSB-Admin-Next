using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class ClicksDataLog
    {
        public int ClickId { get; set; }
        public int LatestCount { get; set; }
        public DateTime ClickDateTime { get; set; }
        public int Startupid { get; set; }
        public string? Userid { get; set; }

        public virtual Startup Startup { get; set; } = null!;
        public virtual User? User { get; set; }
    }
}
