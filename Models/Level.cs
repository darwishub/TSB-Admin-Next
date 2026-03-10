using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public class Level
    {
        [Key]
        public int? IdLevel { get; set; }
        public string? NameLevel { get; set; }
        public int? IdGoalsCategory { get; set; }
        public string? LogoLevel { get; set; }
        public byte? CodeLevel { get; set; }
        public string? Why { get; set; }
        public string? What { get; set; }
    }
}
