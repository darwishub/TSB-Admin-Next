using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class HangOutLink
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public int Mentorsessionid { get; set; }
    }
}
