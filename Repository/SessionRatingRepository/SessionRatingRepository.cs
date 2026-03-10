using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class SessionRatingRepository : RepositoryBase<SessionRating>, ISessionRatingRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public SessionRatingRepository(InvesteurContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<SessionRating>> GetReviewByStartupId(int? startupId)
        {
             var query = (from _review in investeur_context.SessionRating.AsNoTracking()
                         where _review.startupid == startupId
                         select _review).ToList();
            return query;
        }
        public async Task<IEnumerable<SessionRating>> GetAllRatings()
        {
            var query = (from _review in investeur_context.SessionRating.AsNoTracking()
                         select _review).ToList();
            return query;
        }   
    }
}
