using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using TheStartupBuddyV3.Models;
using TheStartupBuddyV3.Repository;
using TheStartupBuddyV3.Service;

namespace TheStartupBuddyV3.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NotificationsController : Controller
    {
        private readonly IRepositoryWrapper _repository;

        public NotificationsController(
        IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        #region save data to notifications
        [HttpPost]
        public async Task<IActionResult> SendNotifications([FromBody] Notifications notification)
        {

            var Notifications = notification;
            var userOwner = await _repository.Startup.GetDataByStartupId(Notifications.StartupId);

            try
            {
                //create notifications for the owner
                var newNotification = new Notifications
                {
                    StartupId = Notifications.StartupId,
                    UserId = userOwner.Userid ?? "",
                    IsAction = Notifications.IsAction,
                    Message = Notifications.Message,
                    Link = Notifications.Link,
                    ReadStatus = false,
                    JoinedAs = Notifications.JoinedAs,
                    Startdate = DateTime.UtcNow,
                    Enddate = DateTime.UtcNow,
                    HasButton = Notifications.HasButton,
                    ButtonLabel = Notifications.ButtonLabel,
                    SuccessMessage = Notifications.SuccessMessage,
                    Redirect = Notifications.Redirect,
                    NotifyToTeam = Notifications.NotifyToTeam,
                    Clicked = false

                };

                _repository.Notifications.CreateNotification(newNotification);
                await _repository.SaveAsync();

                //create notifications for the members
                var members = await _repository.StartupMember.GetAllUserMembers(Notifications.StartupId);

                foreach (var member in members)
                {
                    var newData = new Notifications
                    {
                        StartupId = Notifications.StartupId,
                        UserId = member.UserId ?? "",
                        IsAction = Notifications.IsAction,
                        Message = Notifications.Message,
                        Link = Notifications.Link,
                        ReadStatus = false,
                        JoinedAs = Notifications.JoinedAs,
                        Startdate = DateTime.UtcNow,
                        Enddate = DateTime.UtcNow,
                        HasButton = Notifications.HasButton,
                        ButtonLabel = Notifications.ButtonLabel,
                        SuccessMessage = Notifications.SuccessMessage,
                        Redirect = Notifications.Redirect,
                        NotifyToTeam = Notifications.NotifyToTeam,
                        Clicked = false

                    };

                    _repository.Notifications.CreateNotification(newData);
                    await _repository.SaveAsync();

                }

                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message, confirm = true });

            }

        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> SendNotificationsToEmail([FromBody] NotificationsByEmail notification)
        {

            var NotificationsByEmail = notification;
            var startup = await _repository.Startup.GetStartupIdByEmail(NotificationsByEmail.Email);
            var user = await _repository.User.GetUserByEmailAsync(NotificationsByEmail.Email);
            var userProfile = await _repository.UserProfile.GetUserProfileByUserIdAsync(user.UserId);

            try
            {
                //create notifications for the owner
                var newNotification = new Notifications
                {
                    StartupId = startup.Startupid,
                    UserId = user.UserId ?? "",
                    IsAction = NotificationsByEmail.IsAction,
                    Message = NotificationsByEmail.Message,
                    Link = NotificationsByEmail.Link,
                    ReadStatus = false,
                    JoinedAs = userProfile.Joinedas,
                    Startdate = DateTime.UtcNow,
                    Enddate = DateTime.UtcNow,
                    HasButton = NotificationsByEmail.HasButton,
                    ButtonLabel = NotificationsByEmail.ButtonLabel,
                    SuccessMessage = NotificationsByEmail.SuccessMessage,
                    Redirect = NotificationsByEmail.Redirect,
                    NotifyToTeam = NotificationsByEmail.NotifyToTeam,
                    Clicked = false
                };

                _repository.Notifications.CreateNotification(newNotification);
                await _repository.SaveAsync();

                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message, confirm = true });

            }

        }

        #region update read status notifications
        [HttpPost]
        public async Task<IActionResult> UpdateNotifications(string userid, int startupid)
        {

            try
            {

                var getData = await _repository.Notifications.GetAllMembersAtNotification(startupid, userid);

                if (getData != null)
                {
                    foreach (var item in getData)
                    {
                        var getOneData = await _repository.Notifications.GetOneDataByID(item.ID);
                        getOneData.ReadStatus = true;
                        _repository.Notifications.Update(getOneData);
                        await _repository.SaveAsync();
                    }
                }

                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message, confirm = true });

            }

        }
        #endregion

        #region get all notifications for each user
        [HttpGet]
        public async Task<IActionResult> GetNotifications(string userid, int startupid)
        {

            try
            {

                var notifications = await _repository.Notifications.GetAllMembersAtNotification(startupid, userid);

                string notificationResults = JsonConvert.SerializeObject(notifications, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Content(notificationResults);

            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message, confirm = true });

            }

        }


        [HttpGet]
        public async Task<IActionResult> GetPersonalNotifications(string userid, int startupid)
        {

            try
            {

                var notifications = await _repository.Notifications.GetAllPersonalNotifications(startupid, userid);

                string notificationResults = JsonConvert.SerializeObject(notifications, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Content(notificationResults);

            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message, confirm = true });

            }

        }

        [HttpGet]
        public async Task<IActionResult> GetTeamNotifications(string userid, int startupid)
        {

            try
            {

                var notifications = await _repository.Notifications.GetAllTeamNotifications(startupid, userid);

                string notificationResults = JsonConvert.SerializeObject(notifications, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Content(notificationResults);

            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message, confirm = true });

            }

        }

        #endregion

        #region clear notifications
        [HttpPost]
        public async Task<IActionResult> ClearNotifications(int startupid, string userid)
        {

            try
            {

                var notifications = await _repository.Notifications.GetAllMembersAtNotification(startupid, userid);

                foreach (var item in notifications)
                {
                    var _notification = await _repository.Notifications.GetOneDataByID(item.ID);
                    _repository.Notifications.Delete(_notification);
                    await _repository.SaveAsync();
                }

                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message, confirm = true });

            }

        }
        #endregion

        #region update read status notifications
        public async Task<IActionResult> UpdateMemberStatus(int startupid, string email)
        {
            try
            {
                var getData = await _repository.Startup.GetStartupIdByEmail(email);
                if (getData != null)
                {
                    var getOneData = await _repository.StartupMember.GetMemberByStartupIdEmail(startupid, email);
                    if (getOneData != null)
                    {
                        getOneData.Status = 1;
                        _repository.StartupMember.Update(getOneData);
                        await _repository.SaveAsync();
                    }

                    #region connect member and owner on networking
                    var _networkingData = await _repository.NetworkingConnect.GetDataByUserId(startupid, getData.Startupid);
                    var _networkingData_ = await _repository.NetworkingConnect.GetDataConnect(startupid, getData.Startupid);

                    if (_networkingData == null && _networkingData_ == null)
                    {
                        var newConnect = new NetworkingConnect
                        {
                            SenderId = startupid,
                            ReceiverId = getData.Startupid,
                            RequestDate = DateTime.Now,
                            AccepDate = DateTime.Now,
                            ConnectionStatus = true,
                            RequestBy = startupid
                        };
                        _repository.NetworkingConnect.Create(newConnect);
                        await _repository.SaveAsync();
                    }
                    #endregion
                }
                return Ok("ok");
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message, confirm = true });
            }
        }
        #endregion

        public async Task<IActionResult> UpdateClicked(int id_notification)
        {
            try
            {
                var GetNotification = await _repository.Notifications.GetOneDataByID(id_notification);
                if (GetNotification != null)
                {
                    GetNotification.Clicked = true;
                    _repository.Notifications.Update(GetNotification);
                    await _repository.SaveAsync();
                }
                return Ok("ok");
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message, confirm = true });
            }
        }
    }
}