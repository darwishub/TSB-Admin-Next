using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class FundingFieldValue
    {
        public string Userid { get; set; } = null!;
        public int FieldId { get; set; }
        public string Value { get; set; } = null!;
        public DateTime Lastupdated { get; set; }
    }
}
