using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface INotificationsRepository : IRepositoryBase<Notifications>
    {
        Task<IEnumerable<Notifications>> GetNotificationsByStartupId(int startupid);
        void CreateNotification(Notifications notification);
        void UpdateNotification(Notifications notification);
        void DeleteNotification(Notifications notification);
        Task<IEnumerable<Notifications>> GetAllMembersAtNotification(int? startupid, string? userid);
        Task<IEnumerable<Notifications>> GetAllPersonalNotifications(int? startupid, string? userid);
        Task<IEnumerable<Notifications>> GetAllTeamNotifications(int? startupid, string? userid);
        Task<Notifications> GetOneDataByID(int? ID);
        Task<IEnumerable<Notifications>> GetNotificationsByUserId(string? userId);
    }
}
