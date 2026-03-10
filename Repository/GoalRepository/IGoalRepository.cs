using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{

    public interface IGoalRepository : IRepositoryBase<Goal>
    {
        Task<IEnumerable<Goal>> GetAllGoalAsync();
        Task<Goal> GetGoalByIdAsync(int goalId);
        Task<Goal> GetSingleGoalByPromoGoalStatus(bool status);
        void CreateGoal(Goal goal);
        void UpdateGoal(Goal goal);
        void DeleteGoal(Goal goal);
        Task<Goal> GetGoalsById(int goalId);
        Task<Goal> GetGoalsByStatusProgram(int programId);
    }
}
