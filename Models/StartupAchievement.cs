using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class StartupAchievement
    {
        public int Achievementid { get; set; }
        public int Startupid { get; set; }
        public string Metric { get; set; } = null!;
        public string Change { get; set; } = null!;
        public string Notes { get; set; } = null!;
        public DateTime Datetime { get; set; }
        public int Week { get; set; }
    }
}
