using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class StartupGoal
    {
        public int Goalid { get; set; }
        public int Startupid { get; set; }
        public string Description { get; set; } = null!;
        public DateTime Datetime { get; set; }
        public bool Status { get; set; }
        public DateTime Lastupdated { get; set; }
        public int Week { get; set; }
        public string? AssigneeTo { get; set; }
        public DateTime? DueDate { get; set; }
        public string? UseridAssignee { get; set; }
    }
}
