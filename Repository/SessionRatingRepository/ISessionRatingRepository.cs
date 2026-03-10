using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface ISessionRatingRepository : IRepositoryBase<SessionRating>
    {
        Task<IEnumerable<SessionRating>> GetReviewByStartupId(int? startupId);
        Task<IEnumerable<SessionRating>> GetAllRatings();

    }
}
