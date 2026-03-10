using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class CompanyGoalsStepRepository : RepositoryBase<CompanyGoalsStep>, ICompanyGoalsStepRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public CompanyGoalsStepRepository(InvesteurContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CompanyGoalsStep>> GetStepsByStartupId(int? startupid)
        {
            var getSteps = await (from _step in investeur_context.CompanyGoalsStep.AsNoTracking()
                            where _step.StartupId == startupid && _step.IsStepComplete == true
                            select new CompanyGoalsStep
                            {
                                IdGoalStep = _step.IdCompanyGoalsStep,
                            }).ToListAsync();
            return getSteps;
        }
    }
}