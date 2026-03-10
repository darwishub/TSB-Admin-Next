using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;
namespace TheStartupBuddyV3.Repository
{
    public class AspNetUsersRepository : RepositoryBase<AspNetUser>, IAspNetUsersRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public AspNetUsersRepository(InvesteurContext _context) : base(_context)
        {
        }
        public async Task<IEnumerable<AspNetUser>> GetAllNetUsersAsync()
        {
            return await GetAll()
               .ToListAsync();
        }

        public async Task<AspNetUser> GetNetUserByUserId(string userId)
        {
            return await GetByCondition(data => data.Id == userId).FirstOrDefaultAsync();
        }

        public void CreateNetUser(AspNetUser netUser)
        {
            Create(netUser);
        }
        public void UpdateNetUser(AspNetUser netUser)
        {
            Update(netUser);
        }
        public void DeleteNetUser(AspNetUser netUser)
        {
            Delete(netUser);
        }
        
    }
}
