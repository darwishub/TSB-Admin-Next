using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IAspNetUsersRepository : IRepositoryBase<AspNetUser>
    {
        Task<IEnumerable<AspNetUser>> GetAllNetUsersAsync();
        Task<AspNetUser> GetNetUserByUserId(string userId);
        void CreateNetUser(AspNetUser netUser);
        void UpdateNetUser(AspNetUser netUser);
        void DeleteNetUser(AspNetUser netUser);
    }
}
