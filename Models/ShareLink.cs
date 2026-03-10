using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class ShareLink
    {
        public int Linkid { get; set; }
        public string Token { get; set; } = null!;
        public int Startupid { get; set; }
        public DateTime Datecreated { get; set; }
        public int Validdays { get; set; }
        public string Sharecontent { get; set; } = null!;
        public string Userid { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}
