using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IBenefitRepository : IRepositoryBase<Benefit>
    {
        Task<IEnumerable<Benefit>> GetAllBenefitsAsync();
        Task<Benefit> GetItemById(int? Id);
        void CreateBenefit(Benefit Benefit);
        void UpdateBenefit(Benefit Benefit);
        void DeleteBenefit(Benefit Benefit);
    }
}
