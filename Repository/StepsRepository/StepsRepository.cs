using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class StepsRepository : RepositoryBase<Step>, IStepsRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public StepsRepository(InvesteurContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Step>> GetTotalSteps()
        {
            var _totalSteps = await (from _total in investeur_context.Steps.AsNoTracking()
                                     select new Step
                                     {
                                         IdStep = _total.IdStep,
                                     }).ToListAsync();
            return _totalSteps;
        }

        public async Task<Step> GetStepByIdAsync(int stepId)
        {
            return await GetByCondition(step => step.IdStep.Equals(stepId)).FirstOrDefaultAsync();
        }

        public void CreateStep(Step step)
        {
            Create(step);
        }
        public void UpdateStep(Step step)
        {
            Update(step);
        }
        public void DeleteStep(Step step)
        {
            Delete(step);
        }
    }

}
