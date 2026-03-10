using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface ICompanyGoalsStepRepository : IRepositoryBase<CompanyGoalsStep>
    {
        Task<IEnumerable<CompanyGoalsStep>> GetStepsByStartupId(int? startupid);
    }
}