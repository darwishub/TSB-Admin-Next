using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IGoalsStepRepository : IRepositoryBase<GoalsStep>
    {
        Task<IEnumerable<GoalsStepModel>> GetTotalSteps(int ProgramId, bool statusGoal);
        void CreateGoalStep(GoalsStep goal_step);
        void UpdateGoalStep(GoalsStep goal_step);
        void DeleteGoalStep(GoalsStep goal_step);
        Task<IEnumerable<GoalsStep>> GetAllStepsByGoalIdAsync(int? goalId);
        Task<GoalsStep> GetGoalsStepByIdAsync(int stepId);
        Task<IEnumerable<GoalsStep>> GetAllStepsAsync(int? programid, bool? goalBasics);
    }
}
