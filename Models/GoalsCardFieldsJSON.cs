using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public class GoalsCardFieldsJSON
    {
        [Key]
        public int IdGoal { get; set; }
        public string? Json { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
