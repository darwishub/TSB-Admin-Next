using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class StartupMission
    {
        public int Missionid { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Isbonus { get; set; }
        public short Percentage { get; set; }
        public short Defaultorder { get; set; }
        public string Subtitle { get; set; } = null!;
        public bool Isdisable { get; set; }
        public string? AssignTo { get; set; }
    }
}
