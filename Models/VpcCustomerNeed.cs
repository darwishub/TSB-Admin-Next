using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class VpcCustomerNeed
    {
        public int Customerneedid { get; set; }
        public int Customertypeid { get; set; }
        public string Name { get; set; } = null!;
        public short Type { get; set; }
    }
}
