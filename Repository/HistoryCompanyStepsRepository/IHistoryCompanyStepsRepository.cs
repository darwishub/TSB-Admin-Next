using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IHistoryCompanyStepsRepository : IRepositoryBase<HistoryCompanySteps>
    {
        Task<IEnumerable<HistoryCompanySteps>> GetCompleteStepsByUser(string? email);

        Task<IEnumerable<HistoryCompanySteps>> GetStepsByStartupId(int? startupid);
    }
}
