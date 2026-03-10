using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface ISubscriptionStartupRepository
    {
        Task<IEnumerable<SubscriptionStartup>> GetAllSubscribersAsync();
        Task<SubscriptionStartup> GetSubscriberByUserIdAsync(string? userid);
        void CreateSubscription(SubscriptionStartup subscription);
        void UpdateSubscription(SubscriptionStartup subscription);
        void DeleteSubscription(SubscriptionStartup subscription);
        public int CountSubscriptionByUserIdAsync(string? userid);
    }
}
