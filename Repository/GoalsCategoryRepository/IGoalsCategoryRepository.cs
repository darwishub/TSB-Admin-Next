using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IGoalsCategoryRepository : IRepositoryBase<GoalsCategory>
    {
        Task<IEnumerable<GoalsCategory>> GetGoalsCategory();
    }
}
