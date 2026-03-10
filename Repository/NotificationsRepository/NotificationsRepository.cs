using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class NotificationsRepository : RepositoryBase<Notifications>, INotificationsRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public NotificationsRepository(InvesteurContext _context) : base(_context)
        {
        }

        public void CreateNotification(Notifications notification)
        {
            Create(notification);
        }
        public void UpdateNotification(Notifications notification)
        {
            Update(notification);
        }
        public void DeleteNotification(Notifications notification)
        {
            Delete(notification);
        }

        #region get member join with user table
        public async Task<IEnumerable<Notifications>> GetAllMembersAtNotification(int? startupid, string? userid)
        {
            var query = await (from _notif in investeur_context.Notifications.AsNoTracking()
                               where _notif.StartupId == startupid && _notif.UserId == userid
                               select new Notifications
                               {
                                   ID = _notif.ID,
                                   StartupId = _notif.StartupId,
                                   UserId = _notif.UserId,
                                   IsAction = _notif.IsAction,
                                   Message = _notif.Message,
                                   Link = _notif.Link,
                                   ReadStatus = _notif.ReadStatus,
                                   JoinedAs = _notif.JoinedAs,
                                   Startdate = _notif.Startdate,
                                   Enddate = _notif.Enddate,
                                   HasButton = _notif.HasButton,
                                   ButtonLabel = _notif.ButtonLabel,
                                   SuccessMessage = _notif.SuccessMessage,
                                   Redirect = _notif.Redirect,
                                   NotifyToTeam = _notif.NotifyToTeam,
                                   Clicked = _notif.Clicked
                               }).OrderByDescending(i => i.Startdate).ToListAsync();
            return query;
        }
        #endregion

        public async Task<IEnumerable<Notifications>> GetAllPersonalNotifications(int? startupid, string? userid)
        {
            var query = await (from _notif in investeur_context.Notifications.AsNoTracking()
                               where _notif.StartupId == startupid && _notif.UserId == userid && _notif.NotifyToTeam == false
                               select new Notifications
                               {
                                   ID = _notif.ID,
                                   StartupId = _notif.StartupId,
                                   UserId = _notif.UserId,
                                   IsAction = _notif.IsAction,
                                   Message = _notif.Message,
                                   Link = _notif.Link,
                                   ReadStatus = _notif.ReadStatus,
                                   JoinedAs = _notif.JoinedAs,
                                   Startdate = _notif.Startdate,
                                   Enddate = _notif.Enddate,
                                   HasButton = _notif.HasButton,
                                   ButtonLabel = _notif.ButtonLabel,
                                   SuccessMessage = _notif.SuccessMessage,
                                   Redirect = _notif.Redirect,
                                   NotifyToTeam = _notif.NotifyToTeam,
                                   Clicked = _notif.Clicked
                               }).OrderByDescending(i => i.Startdate).ToListAsync();
            return query;
        }

        public async Task<IEnumerable<Notifications>> GetAllTeamNotifications(int? startupid, string? userid)
        {
            var query = await (from _notif in investeur_context.Notifications.AsNoTracking()
                               where _notif.StartupId == startupid && _notif.UserId == userid && _notif.NotifyToTeam == true
                               select new Notifications
                               {
                                   ID = _notif.ID,
                                   StartupId = _notif.StartupId,
                                   UserId = _notif.UserId,
                                   IsAction = _notif.IsAction,
                                   Message = _notif.Message,
                                   Link = _notif.Link,
                                   ReadStatus = _notif.ReadStatus,
                                   JoinedAs = _notif.JoinedAs,
                                   Startdate = _notif.Startdate,
                                   Enddate = _notif.Enddate,
                                   HasButton = _notif.HasButton,
                                   ButtonLabel = _notif.ButtonLabel,
                                   SuccessMessage = _notif.SuccessMessage,
                                   Redirect = _notif.Redirect,
                                   NotifyToTeam = _notif.NotifyToTeam,
                                   Clicked = _notif.Clicked

                               }).OrderByDescending(i => i.Startdate).ToListAsync();
            return query;
        }

        #region get each data for update status by userid
        public async Task<Notifications> GetOneDataByID(int? ID)
        {
            var query = (from _notif in investeur_context.Notifications.AsNoTracking()
                         where _notif.ID == ID
                         select _notif).ToList().FirstOrDefault();
            return query;
        }
        #endregion

        #region get all notifications by userid
        public async Task<IEnumerable<Notifications>> GetNotificationsByUserId(string? userId)
        {
            var query = await (from _notif in investeur_context.Notifications.AsNoTracking()
                               where _notif.UserId == userId
                               select new Notifications
                               {
                                   ID = _notif.ID,
                                   StartupId = _notif.StartupId,
                                   UserId = _notif.UserId,
                                   IsAction = _notif.IsAction,
                                   Message = _notif.Message,
                                   Link = _notif.Link,
                                   ReadStatus = _notif.ReadStatus,
                                   JoinedAs = _notif.JoinedAs,
                                   Startdate = _notif.Startdate,
                                   Enddate = _notif.Enddate,
                                   HasButton = _notif.HasButton,
                                   ButtonLabel = _notif.ButtonLabel,
                                   SuccessMessage = _notif.SuccessMessage,
                                   Redirect = _notif.Redirect,
                                   NotifyToTeam = _notif.NotifyToTeam,
                                   Clicked = _notif.Clicked

                               }).ToListAsync();
            return query;
        }
        #endregion

        #region get all notifications by startupid
        public async Task<IEnumerable<Notifications>> GetNotificationsByStartupId(int startupid)
        {
            var query = await (from _notif in investeur_context.Notifications.AsNoTracking()
                               where _notif.StartupId == startupid
                               select new Notifications
                               {
                                   ID = _notif.ID,
                                   StartupId = _notif.StartupId,
                                   UserId = _notif.UserId,
                                   IsAction = _notif.IsAction,
                                   Message = _notif.Message,
                                   Link = _notif.Link,
                                   ReadStatus = _notif.ReadStatus,
                                   JoinedAs = _notif.JoinedAs,
                                   Startdate = _notif.Startdate,
                                   Enddate = _notif.Enddate,
                                   HasButton = _notif.HasButton,
                                   ButtonLabel = _notif.ButtonLabel,
                                   SuccessMessage = _notif.SuccessMessage,
                                   Redirect = _notif.Redirect,
                                   NotifyToTeam = _notif.NotifyToTeam,
                                   Clicked = _notif.Clicked

                               }).ToListAsync();
            return query;
        }
        #endregion
    }
}
