using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class CompanyGoalsCardFieldsRepository : RepositoryBase<CompanyGoalsCardFields>, ICompanyGoalsCardFieldsRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public CompanyGoalsCardFieldsRepository(InvesteurContext context) : base(context)
        {
        }
        public void CreateCompanyGoalsCardFields(CompanyGoalsCardFields goalCard)
        {
            Create(goalCard);
        }

        public async Task<CompanyGoalsCardFields> GetCompanyGoalCardFieldsByIdAsync(int cardId, int startupid)
        {
            return await GetByCondition(data => data.IdCard == cardId && data.StartupId == startupid).FirstOrDefaultAsync();
        }

        public void UpdateGoalCardFields(CompanyGoalsCardFields goalCard)
        {
            Update(goalCard);
        }
    }
}
