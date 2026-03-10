using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class StartupDemoDay
    {
        public int Id { get; set; }
        public string Userid { get; set; } = null!;
        public DateTime Applicationdate { get; set; }
        public string Status { get; set; } = null!;
        public int Startupid { get; set; }
        public string Chargeid { get; set; } = null!;
    }
}
