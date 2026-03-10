namespace TheStartupBuddyV3.Models
{
    public class StepByGoal
    {
        public int step { get; set; }
        public string title { get; set; } = null!;
        public string? description { get; set; }
        public byte? is_step_complete { get; set; }
        public int? id_goal { get; set; }
        public List<Cards>? cards { get; set; }
    }
}
