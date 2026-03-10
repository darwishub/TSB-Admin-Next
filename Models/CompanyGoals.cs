using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public class CompanyGoals
    {
        [Key]
        public int IdCompanyGoals { get; set; }
        public int IdGoal { get; set; }
        public int? StartupId { get; set; }
        public string? IdUser { get; set; }
        public bool IsRewardCollected { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class CompanyGoalsStep
    {
        [Key]
        public int IdCompanyGoalsStep { get; set; }
        public int IdGoal { get; set; }
        public int IdGoalStep { get; set; }
        public int? StartupId { get; set; }
        public string? IdUser { get; set; }
        public bool IsStepComplete { get; set; }
        public bool? IsGoalComplete { get; set; }
        public int? TSBCoin { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class Graph
    {
        public int totalCompanies { get; set; }
        public int stepLastTime { get; set; }
        public int stepThisTime { get; set; }
        public Array? labels { get; set; }
        public Array? stepPerPeriod { get; set; }
        public double? percentage { get; set; }

        public Array? listConnectionPerPeriod { get; set; }

        public Array? coinsPerPeriod { get; set; }
    }

    public class TopGraph
    {
        public Array? labels { get; set; }
        public Array? stepPerDay { get; set; }
        public double? percentage { get; set; }
        public Array? listConnectionPerDay { get; set; }
        public Array? coinsPerDay { get; set; }
    }

    public class GraphProfile
    {
        public int? totalStep { get; set; }
        public int? stepTaken { get; set; }
        public int stepLastTime { get; set; }
        public int stepThisTime { get; set; }
        public Array? labels { get; set; }
        public Array? stepPerPeriod { get; set; }
        public Array? listConnectionPerPeriod { get; set; }
        public Array? coinsPerPeriod { get; set; }
        public double? percentage { get; set; }
        
    }

    public class CompanyGoalsWithCoins
    {
        [Key]
        public int id_company_goals { get; set; }
        public int id_goal { get; set; }
        public int? startupid { get; set; }
        public bool is_reward_collected { get; set; }
        public DateTime date_created { get; set; }
        public int? coins { get; set; }
    }
}
