using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public class Goal
    {
        [Key]
        public int IdGoal { get; set; }
        public string? TitleGoal { get; set; }
        public int? IdLevel { get; set; }
        public bool? IsRequired { get; set; }
        public string? Logo { get; set; }
        public string? HelpText { get; set; }
        public int? TotalPoints { get; set; }
        public bool? LockedStatus { get; set; }
        public bool? StatusComplete { get; set; }
        public bool? StatusPromo { get; set; }
        public bool StatusOnboarding { get; set; }
        public string? Description { get; set; }
        public string? SecondTitle { get; set; }
        public string? Label { get; set; }
        public DateTime DateCreated { get; set; }
        public string? StepsJsonUI { get; set; }
        public int ProgramId { get; set; }
        public int ProgramGroupId { get; set; }
        public bool? IsPublished { get; set; }
        public bool? StatusScore { get; set; }
    }

    public class GoalDetailsCards
    {
        public int id { get; set; }
        public string? help_text { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public GoalDetailsSchema? schema { get; set; }
        public int? score { get; set; }
        public int? origin_score { get; set; }
    }

    public class GoalDetailsProps
    {
        public string? placeholder { get; set; }
        public string? text_button { get; set; }
        public string? type { get; set; }
        public string? type_rule { get; set; }
        public string? label { get; set; }
        public string? value { get; set; }
        public bool required { get; set; }
        public bool disabled { get; set; }
        public int max_data { get; set; }
        public bool allow_multiple { get; set; }
        public string[]? files_type { get; set; }
        public int score { get; set; }
    }

    public class GoalDetails
    {
        public int? id { get; set; }
        public string? logo { get; set; }
        public string? label { get; set; }
        public int id_level { get; set; }
        public int? code_level { get; set; }
        public int? id_goal_category { get; set; }
        public List<GoalDetailsStep>? steps { get; set; }
        public string? title { get; set; }
        public bool required { get; set; }
        public bool? promo_goal { get; set; }
        public bool? onboarding_goal { get; set; }
        public bool is_locked { get; set; }
        public string? description { get; set; }
        public string? second_title { get; set; }
        public int total_points { get; set; }
        public bool is_goal_complete { get; set; }
        public string? goal_category_logo { get; set; }
        public string? goal_help_text { get; set; }
        public bool? is_published { get; set; }
        public string? cards_ui_json { get; set; }
        public bool? status_score { get; set; }
        public int? score { get; set;}
        public int program_group_id { get; set;}
    }

    public class GoalDetailsSchema
    {
        public int field_code { get; set; }
        public int field_code_type { get; set; }
        public GoalDetailsProps? props { get; set; }
        public bool has_option { get; set; }
        public string[]? option { get; set; }
        public int display_order { get; set; }
        public string? success_message { get; set; }
    }

    public class GoalDetailsStep
    {
        public int step { get; set; }
        public int id_goal_step { get; set; }
        public string? step_title { get; set; }
        public string? step_description { get; set; }
        public bool is_step_complete { get; set; }
        public List<GoalDetailsCards>? cards { get; set; }
    }

    public class Logos 
    {
        public int? id_level { get; set; }
        public string? logo { get; set; }
        public int? code_level { get; set; }
        public int? id_category { get; set; }
    }

}
