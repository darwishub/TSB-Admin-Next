using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public partial class GetAllGoals
    {
        [Key]
        public int id { get; set; }
        public string? logo { get; set; }
        public string? label { get; set; }
        public int? level { get; set; }
        public string? title { get; set; }
        public bool? required { get; set; }
        public bool? is_locked { get; set; }
        public string? description { get; set; }
        public string? second_title { get; set; }
        public int? total_points { get; set; }
        public bool? is_goal_complete { get; set; }
        public string? goal_category_logo { get; set; }
        public int? id_category { get; set; }
    }

    public partial class GetAllGoalsWithSteps
    {
        [Key]
        public int id { get; set; }
        public string? logo { get; set; }
        public string? label { get; set; }
        public int? level { get; set; }
        public IList<Steps> steps { get; set; }
        public string? title { get; set; }
        public bool? required { get; set; }
        public bool? is_locked { get; set; }
        public string? description { get; set; }
        public string? second_title { get; set; }
        public int? total_points { get; set; }
        public bool? is_goal_complete { get; set; }
        public string? goal_category_logo { get; set; }
        public int? id_category { get; set; }
        public Byte? code_level { get; set; }
        public int programid { get; set; }
        public bool? is_published { get; set; }
        public int program_group_id { get; set; }
    }
}
