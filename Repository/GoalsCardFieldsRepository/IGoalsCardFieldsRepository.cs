using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{

    public interface IGoalsCardFieldsRepository : IRepositoryBase<GoalsCardFields>
    {
        void CreateGoalCard(GoalsCardFields goal_card);
        void UpdateGoalCard(GoalsCardFields goal_card);
        void DeleteGoalCard(GoalsCardFields goal_card);
        Task<GoalsCardFields> GetGoalCardByIdAsync(int cardId);
        Task<IEnumerable<GoalsCardFields>> GetGoalCardByIdStep(int idStep);
        Task<IEnumerable<GoalsCardFields>> GetAllData(int? currentGoalId);
        Task<IEnumerable<GoalsCardFields>> GetAllDataRecord();
    }
}
