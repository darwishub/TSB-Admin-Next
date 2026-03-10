using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class SessionRating
    {
        public int id_rating { get; set; }
        public string? id_calendar { get; set; }
        public int? points { get; set; }
        public string? comment_review { get; set; }
        public int? coin_bonus { get; set; }
        public int? startupid { get; set; }
    }
}
