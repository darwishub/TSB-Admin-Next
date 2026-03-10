using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public class GoalsCategory
    {
        [Key]
        public int IdGoalsCategory { get; set; }
        public string? NameGoalsCategory { get; set; }
        public string? LogoCategory { get; set; }
        public string? MoneyCategory { get; set; }
    }
}
