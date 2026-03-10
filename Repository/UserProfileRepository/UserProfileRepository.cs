using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class UserProfileRepository : RepositoryBase<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(InvesteurContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<UserProfile>> GetAllUserProfilesAsync()
        {
            return await GetAll()
               .ToListAsync();
        }

        public async Task<UserProfile> GetUserProfileByUserIdAsync(string userId)
        {
            return await GetByCondition(profile => profile.Userid == userId).FirstOrDefaultAsync();
        }

        public void CreateUserProfile(UserProfile userProfile)
        {
            Create(userProfile);
        }
        public void UpdateUserProfile(UserProfile userProfile)
        {
            Update(userProfile);
        }
        public void DeleteUserProfile(UserProfile userProfile)
        {
            Delete(userProfile);
        }
    }
}
