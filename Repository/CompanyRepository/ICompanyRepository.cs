using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        Task<IEnumerable<Company>> GetAllCompanyAsync();
        
    }
}
