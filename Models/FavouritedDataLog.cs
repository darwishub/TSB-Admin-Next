using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class FavouritedDataLog
    {
        public int FavId { get; set; }
        public int Startupid { get; set; }
        public string Userid { get; set; } = null!;
        public short Status { get; set; }
        public DateTime DateTimeFav { get; set; }

        public virtual Startup Startup { get; set; } = null!;
    }
}
