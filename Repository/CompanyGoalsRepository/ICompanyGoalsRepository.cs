using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface ICompanyGoalsRepository : IRepositoryBase<CompanyGoals>
    {
        Task<IEnumerable<Company>> GetAllCompanyAsync();
        Task<IEnumerable<CompanyGoalsStep>> GetDataByStartupId(int? startupid);
        Task<IEnumerable<CompanyGoalsStep>> GetAllCompanySteps(int? programid);
        Task<IEnumerable<CompanyGoalsWithCoins>> GetAllCoinsByProgram(int? programid);
        Task<IEnumerable<CompanyGoalsWithCoins>> GetCoins(int? startupid);
        Task<IEnumerable<CompanyGoalsStep>> GetCompanySteps(int? programid, int? startupid);
    }
}
