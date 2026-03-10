using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IGoalsRepository : IRepositoryBase<Goal>
    {
        Task<IEnumerable<GetAllGoalsWithSteps>> GetAllGoals();
        Task<IEnumerable<CompanyGoalStep>> GetCompleteGoals(string? startupid);
        Task<IEnumerable<GetAllGoals>> GetGoalsById(int? idGoal);
        Task<Goal> GetGoalsByStatusProgram(int programId);
    }
}
