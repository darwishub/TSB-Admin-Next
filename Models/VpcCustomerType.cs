using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class VpcCustomerType
    {
        public int Customertypeid { get; set; }
        public string Name { get; set; } = null!;
        public int Startupid { get; set; }
        public DateTime Createdat { get; set; }
    }
}
