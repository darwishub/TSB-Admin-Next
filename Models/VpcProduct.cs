using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class VpcProduct
    {
        public int Productid { get; set; }
        public int Customertypeid { get; set; }
        public string Name { get; set; } = null!;
    }
}
