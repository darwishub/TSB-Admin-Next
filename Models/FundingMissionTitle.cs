using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class FundingMissionTitle
    {
        public int MissionId { get; set; }
        public string Name { get; set; } = null!;
        public short DefaultOrder { get; set; }
        public bool IsDisable { get; set; }
    }
}
