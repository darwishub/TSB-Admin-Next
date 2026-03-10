using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IUserProfileRepository : IRepositoryBase<UserProfile>
    {
        Task<IEnumerable<UserProfile>> GetAllUserProfilesAsync();
        Task<UserProfile> GetUserProfileByUserIdAsync(string userId);
        void CreateUserProfile(UserProfile userProfile);
        void UpdateUserProfile(UserProfile userProfile);
        void DeleteUserProfile(UserProfile userProfile);
    }
}
