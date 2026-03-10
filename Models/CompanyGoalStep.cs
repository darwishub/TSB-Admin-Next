namespace TheStartupBuddyV3.Models
{
    public class CompanyGoalStep
    {
        public int? id_goal_step { get; set; }
        public int? id_goal { get; set; }
        public bool? is_step_complete { get; set; }
        public DateTime? dateCreated { get; set; }
        public int? score { get; set; }
        public int? startupId { get; set; }
    }
}
