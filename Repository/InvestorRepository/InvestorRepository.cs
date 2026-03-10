using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class InvestorRepository : RepositoryBase<Investor>, IInvestorRepository
    {
        public InvestorRepository(InvesteurContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<Investor>> GetAllInvestorsAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<Investor> GetInvestorByUserIdAsync(string? userid)
        {
            return await GetByCondition(investor => investor.UserId == userid).FirstOrDefaultAsync();
        }

        public void CreateInvestor(Investor investor)
        {
            Create(investor);
        }
        public void UpdateInvestor(Investor investor)
        {
            Update(investor);
        }
        public void DeleteInvestor(Investor investor)
        {
            Delete(investor);
        }

    }
}
