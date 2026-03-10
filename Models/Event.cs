namespace TheStartupBuddyV3.Models
{
    public partial class Event
    {
        public int Id { get; set; }
        public string? EventName { get; set; }
        public string? Logo { get; set; }
        public string? EventDescription { get; set; }
        public string? EventInfo { get; set; }
        public byte? ApplyTo { get; set; }
        public DateTime? EventDateStart { get; set; }
        public DateTime? EventDateEnd { get; set; }
        public string? EventUrl { get; set; }
        public byte? EventType { get; set; }
        public string? EventAddress { get; set; }
        public byte? EventPlatform { get; set; }
        public int? AssignToProgram { get; set; }
        public byte? EmailNotification { get; set; }
        public byte? ActionType { get; set; }
        public int? PriceEvent { get; set; }
        public bool IsPublished { get; set; }
        public string? tags { get; set; }
        public int? AssignToLevel { get; set; }
        public int? AssignToCategory { get; set; }
        public DateTime? CreateDate { get; set; }
        public int ProgramGroupId { get; set; }
    }
}