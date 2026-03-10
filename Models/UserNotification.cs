using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class UserNotification
    {
        public string Userid { get; set; } = null!;
        public int Notificationid { get; set; }
        public DateTime Datetime { get; set; }
    }
}
