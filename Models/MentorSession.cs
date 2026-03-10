using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class MentorSession
    {
        public int Sessionid { get; set; }
        public int Mentorid { get; set; }
        public string Userid { get; set; } = null!;
        public short Status { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }
        public string Hangout { get; set; } = null!;
    }
}
