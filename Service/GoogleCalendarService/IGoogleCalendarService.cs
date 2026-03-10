using Google.Apis.Calendar.v3.Data;

public interface IGoogleCalendarService
{
    Task<Event> GetGoogleCalendarEvent(string? eventId, CancellationToken cancellationToken);
    Task<Events> GetListCalendars(CancellationToken cancellationToken);
    Task<string> GetMeetLogs(CancellationToken cancellationToken);
}