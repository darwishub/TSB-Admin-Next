using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class SubscriptionInvestorRepository : RepositoryBase<SubscriptionInvestor>, ISubscriptionInvestorRepository
    {
        public SubscriptionInvestorRepository(InvesteurContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<SubscriptionInvestor>> GetAllSubscribersAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<SubscriptionInvestor> GetSubscriberByUserIdAsync(string? userid)
        {
            return await GetByCondition(subscriber => subscriber.Userid == userid).FirstOrDefaultAsync();
        }

        public void CreateSubscription(SubscriptionInvestor subscription)
        {
            Create(subscription);
        }
        public void UpdateSubscription(SubscriptionInvestor subscription)
        {
            Update(subscription);
        }
        public void DeleteSubscription(SubscriptionInvestor subscription)
        {
            Delete(subscription);
        }
        public int CountSubscriptionByUserIdAsync(string? userid)
        {
            return GetByCondition(subscriber => subscriber.Userid == userid).Count();
        }
    }
}
