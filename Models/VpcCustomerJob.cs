using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class VpcCustomerJob
    {
        public int Customerjobid { get; set; }
        public string Name { get; set; } = null!;
        public int Customertypeid { get; set; }
        public DateTime Createdat { get; set; }
    }
}
