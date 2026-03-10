using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public class HistoryCompanySteps
    {
        [Key]
        public int Id { get; set; }
        public int IdGoalStep { get; set; }
        public int IdCompanyGoals { get; set; }
        public int? StartupId { get; set; }
        public string? IdUser { get; set; }
        public bool? IsStepComplete { get; set; }
        public bool? IsGoalComplete { get; set; }
        public int TSBCoin { get; set; }
        public DateTime? DateTimeHistory { get; set; }
    }
}
