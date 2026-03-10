using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface ICoinHistoryRepository : IRepositoryBase<CoinHistory>
    {
        Task<IEnumerable<CoinHistory>> GetAllHistoryByStartup(int? startupId);
    }
}
