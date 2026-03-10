using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class Enterprise
    {
        public int Id { get; set; }
        public string Enteprise { get; set; } = null!;
        public DateTime? Expirydate { get; set; }
        public string Source { get; set; } = null!;
    }
}
