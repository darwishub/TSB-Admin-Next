using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public class GoalsStep
    {
        [Key]
        public int IdGoalStep { get; set; }
        public int? IdGoal { get; set; }
        public int? IdStep { get; set; }
        public DateTime GoalStepDateCreated { get; set; }
        public int OrderStep { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Boolean Visible { get; set; }

    }

    public class GoalsStepModel
    {
        [Key]
        public int IdGoalStep { get; set; }
        public int? IdGoal { get; set; }
        public int? IdStep { get; set; }
        public DateTime GoalStepDateCreated { get; set; }
        public int OrderStep { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int ProgramGroupId { get; set; }
        public int ProgramId { get; set; }
    }
}