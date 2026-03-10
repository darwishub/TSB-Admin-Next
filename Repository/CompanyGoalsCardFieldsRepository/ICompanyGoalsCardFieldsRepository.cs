using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface ICompanyGoalsCardFieldsRepository : IRepositoryBase<CompanyGoalsCardFields>
    {
        void CreateCompanyGoalsCardFields(CompanyGoalsCardFields goalCard);
        Task<CompanyGoalsCardFields> GetCompanyGoalCardFieldsByIdAsync(int cardId, int startupid);
        void UpdateGoalCardFields(CompanyGoalsCardFields goalCard);
    }
}
