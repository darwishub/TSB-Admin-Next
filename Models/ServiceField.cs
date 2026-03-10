using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class ServiceField
    {
        public int Serviceid { get; set; }
        public string Fieldname { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int Fieldid { get; set; }
    }
}
