using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IAdminUsersRepository : IRepositoryBase<AdminUser1>
    {
        Task<AdminUser1> GetAdminByEmail(string? email);
    }
}
