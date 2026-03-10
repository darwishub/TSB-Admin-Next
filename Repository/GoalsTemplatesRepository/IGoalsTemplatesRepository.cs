using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IGoalsTemplatesRepository : IRepositoryBase<GoalsTemplates>
    {
        Task<IEnumerable<GoalsTemplates>> GetAllGoalsTemplates();
    }
}
