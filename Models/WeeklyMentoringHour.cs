using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class WeeklyMentoringHour
    {
        public int WeeklyMentorId { get; set; }
        public int MentorId { get; set; }
        public byte WeeklyDay { get; set; }
        public DateTime WeeklyStarttime { get; set; }
        public DateTime WeeklyEndtime { get; set; }
    }
}
