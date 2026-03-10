namespace TheStartupBuddyV3.Models
{
    public partial class Notifications
    {
        public int ID { get; set; }
        public int StartupId { get; set; }
        public string? UserId { get; set; }
        public Boolean IsAction { get; set; }
        public string? Message { get; set; }
        public string? Link { get; set; }
        public Boolean ReadStatus { get; set; }
        public string? JoinedAs { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public Boolean HasButton { get; set; }
        public string? ButtonLabel { get; set; }
        public string? SuccessMessage { get; set; }
        public string? Redirect { get; set; }
        public Boolean NotifyToTeam { get; set; }
        public Boolean Clicked { get; set; }

    }

    public partial class NotificationsByEmail
    {
        public string? Email { get; set; }
        public int ID { get; set; }
        public int StartupId { get; set; }
        public string? UserId { get; set; }
        public Boolean IsAction { get; set; }
        public string? Message { get; set; }
        public string? Link { get; set; }
        public Boolean ReadStatus { get; set; }
        public string? JoinedAs { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public Boolean HasButton { get; set; }
        public string? ButtonLabel { get; set; }
        public string? SuccessMessage { get; set; }
        public string? Redirect { get; set; }
        public Boolean NotifyToTeam { get; set; }
        public Boolean Clicked { get; set; }
    }
}