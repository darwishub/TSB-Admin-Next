using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public partial class SubscriptionStartup
    {
        [Key]
        public string Subscriptionid { get; set; } = null!;
        public string Userid { get; set; } = null!;
        public int Status { get; set; }
        public DateTime Starttime { get; set; }
        public short Plantype { get; set; }
        public DateTime? Enddate { get; set; }
        public short? Duration { get; set; }
        public short? Defaultplan { get; set; }
    }
}
