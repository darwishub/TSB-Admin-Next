using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IStepsRepository : IRepositoryBase<Step>
    {
        Task<IEnumerable<Step>> GetTotalSteps();
        Task<Step> GetStepByIdAsync(int stepId);
        void CreateStep(Step step);
        void UpdateStep(Step step);
        void DeleteStep(Step step);

    }

}
