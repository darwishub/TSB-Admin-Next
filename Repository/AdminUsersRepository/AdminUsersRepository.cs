using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class AdminUsersRepository : RepositoryBase<AdminUser1>, IAdminUsersRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public AdminUsersRepository(InvesteurContext context) : base(context)
        {
        }
        public async Task<AdminUser1> GetAdminByEmail(string? email)
        {
            return await GetByCondition(admin => admin.Email == email).FirstOrDefaultAsync();
        }
    }
}
