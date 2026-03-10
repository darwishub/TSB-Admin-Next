using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface ISubscriptionInvestorRepository
    {
        Task<IEnumerable<SubscriptionInvestor>> GetAllSubscribersAsync();
        Task<SubscriptionInvestor> GetSubscriberByUserIdAsync(string? userid);
        void CreateSubscription(SubscriptionInvestor subscription);
        void UpdateSubscription(SubscriptionInvestor subscription);
        void DeleteSubscription(SubscriptionInvestor subscription);
        public int CountSubscriptionByUserIdAsync(string? userid);
    }
}
