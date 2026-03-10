using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class HistoryCompanyStepsRepository : RepositoryBase<HistoryCompanySteps>, IHistoryCompanyStepsRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public HistoryCompanyStepsRepository(InvesteurContext context) : base(context)
        {
        }

        public async Task<IEnumerable<HistoryCompanySteps>> GetCompleteStepsByUser(string? email)
        {
            var completeStep = await (from _step in investeur_context.HistoryCompanySteps.AsNoTracking()
                                join _startup in investeur_context.UsersData.AsNoTracking()
                                on _step.IdUser equals _startup.UserId
                                where _startup.Email == email
                                select new HistoryCompanySteps
                                {
                                    Id = _step.Id,
                                }).ToListAsync();
            return completeStep;
        }

        public async Task<IEnumerable<HistoryCompanySteps>> GetStepsByStartupId(int? startupid)
        {
            var getSteps = await (from _step in investeur_context.HistoryCompanySteps.AsNoTracking()
                            where _step.StartupId == startupid && _step.IsStepComplete == true
                            select new HistoryCompanySteps
                            {
                                Id = _step.Id,
                            }).ToListAsync();
            return getSteps;
        }

    }
}
