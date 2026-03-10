namespace TheStartupBuddyV3.Helpers
{
    public interface IHelpers
    {
        string GetPlanIdStartup(short membership_type, short membership_duration);
        string GetPlanIdInvestor(short membership_type, short membership_duration);
        void StripeCreateSubscription(string role, string userid, string? token, short membership_type, string? coupon, short membership_duration, string email, int count_subscription, bool skip_trial = false);
        string GenerateToken(string email, string userId);
        string GetBaseUrl();
        string RegexFilter(string FileName);
    }
}
