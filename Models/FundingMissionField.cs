using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class FundingMissionField
    {
        public int FieldId { get; set; }
        public int MissionId { get; set; }
        public string TitleName { get; set; } = null!;
        public string ControlType { get; set; } = null!;
        public string? Options { get; set; }
        public short? DisplayOrder { get; set; }
        public bool? IsDisable { get; set; }
        public bool IsRequired { get; set; }
    }
}
