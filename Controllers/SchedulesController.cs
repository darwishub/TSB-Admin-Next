using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TheStartupBuddyV3.Repository;

public static class DateTimeExtensions
{
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }
}

namespace TheStartupBuddyV3.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SchedulesController : Controller
    {
        private readonly IGoogleCalendarService _googleCalendarService;
        private readonly IGoogleCalendarSettings _settings;
        private readonly IRepositoryWrapper _repository;

        public SchedulesController(IGoogleCalendarService googleCalendarService,
        IGoogleCalendarSettings settings,
        IRepositoryWrapper repository)
        {
            _googleCalendarService = googleCalendarService;
            _settings = settings;
            _repository = repository;
        }

        #region get google calendar by eventid
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetDetailEvent(string? eventId, CancellationToken cancellationToken)
        {
            var googleData = await _googleCalendarService.GetGoogleCalendarEvent(eventId, cancellationToken);
            string _benefitResults = JsonConvert.SerializeObject(googleData, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });

            return Content(_benefitResults);
        }

        #endregion

        #region get list calendar
        [HttpGet]
        public async Task<IActionResult> GetListCalendars(CancellationToken cancellationToken, int? programId)
        {
            try
            {
                var listCalendars = await _googleCalendarService.GetListCalendars(cancellationToken);
                if (listCalendars?.Items.Count > 0)
                {
                    listCalendars.Items = listCalendars.Items
                        .Where(x => x?.Attendees != null && x.ExtendedProperties.Shared != null
                        && x.ExtendedProperties.Shared["programId"] == programId.ToString())
                        .ToList();
                }
                var result = new
                {
                    Calendars = listCalendars
                };

                string Results = JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Content(Results);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        #endregion

        #region get list calendar
        [HttpGet]
        public async Task<IActionResult> GetLogsCalendar(CancellationToken cancellationToken)
        {
            try
            {
                var logsCalendar = await _googleCalendarService.GetMeetLogs(cancellationToken);
                var result = new
                {
                    Calendars = logsCalendar
                };

                string Results = JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Content(Results);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        #endregion

    }
}
