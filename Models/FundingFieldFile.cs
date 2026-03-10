using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class FundingFieldFile
    {
        public string Userid { get; set; } = null!;
        public int FieldId { get; set; }
        public string FileName { get; set; } = null!;
        public byte[]? Data { get; set; }
        public string Type { get; set; } = null!;
        public DateTime Lastupdated { get; set; }
    }
}
