using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class BenefitRepository : RepositoryBase<Benefit>, IBenefitRepository
    {
        public BenefitRepository(InvesteurContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<Benefit>> GetAllBenefitsAsync()
        {
            return await GetAll()
               .ToListAsync();
        }

        public async Task<Benefit> GetItemById(int? Id)
        {
            return await GetByCondition(data => data.Id == Id).FirstOrDefaultAsync();
        }

        public void CreateBenefit(Benefit Benefit)
        {
            Create(Benefit);
        }
        public void UpdateBenefit(Benefit Benefit)
        {
            Update(Benefit);
        }
        public void DeleteBenefit(Benefit Benefit)
        {
            Delete(Benefit);
        }

    }
}
