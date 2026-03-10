using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class Notification
    {
        public int Notificationid { get; set; }
        public string Message { get; set; } = null!;
        public string Button { get; set; } = null!;
        public string Link { get; set; } = null!;
        public DateTime Datecreated { get; set; }
        public DateTime Expirydate { get; set; }
        public short Usertype { get; set; }
    }
}
