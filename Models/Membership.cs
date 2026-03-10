using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class Membership
    {
        public string MembershipId { get; set; } = null!;
        public string Userid { get; set; } = null!;
        public short PlanId { get; set; }
        public short PlanDuration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
