using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface ILevelsRepository : IRepositoryBase<Level>
    {
        Task<IEnumerable<Level>> GetLevels();
        Task<IEnumerable<Level>> GetDataByStartupId(int? idGoalStep);
        Task<IEnumerable<Level>> GetLevelsZero();
        Task<Level> GetLevelById(int? levelId);
        Task<IEnumerable<Level>> GetLevelsByCategory(int? idCategory);
        Task<Logos> GetLogoByLevel(int? id_goals_category, int? code_level);
    }
}
