using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Oauth2.v2;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Google;
using System.Net;
using Google.Apis.Http;
using Newtonsoft.Json;
using Google.Apis.Admin.Reports.reports_v1;
using Google.Apis.Services;
using static Google.Apis.Admin.Reports.reports_v1.ActivitiesResource.ListRequest;

public class GoogleCalendarService : IGoogleCalendarService
{
    private readonly IGoogleCalendarSettings _settings;
    private readonly IConfiguration _configuration;

    public GoogleCalendarService(IGoogleCalendarSettings settings,
        IConfiguration configuration
    )
    {
        _settings = settings;
        _configuration = configuration;
    }

    #region get list calendars events
    public async Task<Events> GetListCalendars(CancellationToken cancellationToken)
    {
        string FileName = _configuration["GoogleCalendarServiceAccount:FileName"];
        string CalendarId = _configuration["GoogleCalendarServiceAccount:CalendarId"];

        GoogleCredential credential;
        using (var stream = new FileStream($"{Directory.GetCurrentDirectory()}/{FileName}", FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream)
                .CreateScoped(_settings.Scope);
        }

        var service = new CalendarService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = _settings.ApplicationName,
        });

        // Retrieve all events from the calendar
        EventsResource.ListRequest request = service.Events.List(CalendarId);
        request.ShowDeleted = false;
        // request.TimeMin = DateTime.Now;
        request.SingleEvents = true;
        request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

        var events = await request.ExecuteAsync();

        return events;
    }
    #endregion

    public async Task<string> GetMeetLogs(CancellationToken cancellationToken)
    {
        string FileName = _configuration["GoogleCalendarServiceAccount:FileName"];
        string[] Scopes = { "https://www.googleapis.com/auth/admin.reports.audit.readonly" };

        ServiceAccountCredential credential;
        using (var stream = new FileStream($"{Directory.GetCurrentDirectory()}/{FileName}", FileMode.Open, FileAccess.Read))
        {
            var googleCredential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            var serviceCredential = (ServiceAccountCredential)googleCredential.UnderlyingCredential;
            var initializer = new ServiceAccountCredential.Initializer(serviceCredential.Id)
            {
                User = "faiseh@thestartupbuddy.co",
                Key = serviceCredential.Key,
                Scopes = Scopes
            };
            credential = new ServiceAccountCredential(initializer);
        }

       var service = new ReportsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = _settings.ApplicationName,
        });

        var request = service.Activities.List("all", ApplicationNameEnum.Meet); 
        request.EventName = "call_ended";
        request.MaxResults = 250;

        var activities = await request.ExecuteAsync();

        // var meetingDurations = activities.Items.Select(activity =>
        // {
        //     var durationParameter = activity.Events[0].Parameters.FirstOrDefault(parameter => parameter.Name == "duration");
        //     return durationParameter != null ? int.Parse(durationParameter.Value) : 0;
        // }).ToList();
        // var content = JsonConvert.SerializeObject(activities);

        var completedVideoCalls = activities.Items.Where(activity =>
        {
            var conferenceIdParameter = activity.Events[0].Parameters.FirstOrDefault(parameter => parameter.Name == "conference_id");
            return conferenceIdParameter != null && !string.IsNullOrEmpty(conferenceIdParameter.Value);
        }).ToList();

        var content = JsonConvert.SerializeObject(completedVideoCalls);

        return content;
    }

    #region get event by eventid
    public async Task<Event> GetGoogleCalendarEvent(string? eventId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(eventId))
        {
            throw new ArgumentNullException(nameof(eventId), "Event ID cannot be null or empty.");
        }

        UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
            new ClientSecrets()
            {
                ClientId = _settings.ClientId,
                ClientSecret = _settings.ClientSecret
            },
            _settings.Scope,
            _settings.User,
            cancellationToken);

        var service = new CalendarService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = _settings.ApplicationName,
        });

        var request = service.Events.List(_settings.CalendarId);
        request.TimeMin = DateTime.Now;

        var events = await request.ExecuteAsync(cancellationToken);
        var matchingEvent = events.Items.FirstOrDefault(e => e.Id == eventId);
        return matchingEvent;
    }
    #endregion
}

