using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByEmailAsync(string? email);
        Task<User> GetUserByIdAsync(string? userId);
        Task<User> GetUserByEmailAndTokenAsync(string? email, string? refreshToken);
        Task<List<UserProfile>> GetStartupsRecommendation(int startupId);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
