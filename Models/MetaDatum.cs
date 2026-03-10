using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class MetaDatum
    {
        public int Id { get; set; }
        public string Page { get; set; } = null!;
        public string MetaKey { get; set; } = null!;
        public string Data { get; set; } = null!;
    }
}
