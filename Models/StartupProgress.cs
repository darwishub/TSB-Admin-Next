using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class StartupProgress
    {
        public int Progressid { get; set; }
        public string Userid { get; set; } = null!;
        public int Progress { get; set; }
        public DateTime Updatedat { get; set; }
        public int Companyid { get; set; }
    }
}
