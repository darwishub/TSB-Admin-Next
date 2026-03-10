using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class VpcFeature
    {
        public int Featureid { get; set; }
        public int Customerneedid { get; set; }
        public string Name { get; set; } = null!;
    }
}
