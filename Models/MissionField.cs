using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class MissionField
    {
        public int Fieldid { get; set; }
        public int Missionid { get; set; }
        public string Title { get; set; } = null!;
        public string Controltype { get; set; } = null!;
        public string? Options { get; set; }
        public short? Displayorder { get; set; }
        public bool? Isdisable { get; set; }
        public string Popovertitle { get; set; } = null!;
        public string Popovercontent { get; set; } = null!;
        public bool? Isrequired { get; set; }
    }
}
