using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class UserFieldValue
    {
        public string Userid { get; set; } = null!;
        public int Fieldid { get; set; }
        public string Value { get; set; } = null!;
        public int Companyid { get; set; }
        public DateTime Lastupdated { get; set; }
        public string Fieldname { get; set; } = null!;
        public int Startupid { get; set; }
    }
}
