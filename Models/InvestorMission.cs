using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class InvestorMission
    {
        public string Userid { get; set; } = null!;
        public int Missionid { get; set; }
        public short Status { get; set; }
        public short Displayorder { get; set; }
    }
}
