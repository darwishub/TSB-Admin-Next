using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IInvestorRepository : IRepositoryBase<Investor>
    {
        Task<IEnumerable<Investor>> GetAllInvestorsAsync();
        Task<Investor> GetInvestorByUserIdAsync(string? userid);
        void CreateInvestor(Investor investor);
        void UpdateInvestor(Investor investor);
        void DeleteInvestor(Investor investor);
    }
}
