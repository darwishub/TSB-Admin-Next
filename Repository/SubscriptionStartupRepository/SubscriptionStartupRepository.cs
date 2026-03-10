using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class SubscriptionStartupRepository : RepositoryBase<SubscriptionStartup>, ISubscriptionStartupRepository
    {
        public SubscriptionStartupRepository(InvesteurContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<SubscriptionStartup>> GetAllSubscribersAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<SubscriptionStartup> GetSubscriberByUserIdAsync(string? userid)
        {
            return await GetByCondition(subscriber => subscriber.Userid == userid).FirstOrDefaultAsync();
        }

        public void CreateSubscription(SubscriptionStartup subscription)
        {
            Create(subscription);
        }
        public void UpdateSubscription(SubscriptionStartup subscription)
        {
            Update(subscription);
        }
        public void DeleteSubscription(SubscriptionStartup subscription)
        {
            Delete(subscription);
        }
        public int CountSubscriptionByUserIdAsync(string? userid)
        {
            return GetByCondition(subscriber => subscriber.Userid == userid).Count();
        }
    }
}
