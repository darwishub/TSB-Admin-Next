namespace TheStartupBuddyV3.Models
{
    public class CardsStep
    {
        public int id { get; set; }
        public int? id_step { get; set; }
        public string? help_text { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public Schema? schema { get; set; }
    }

    public class Steps
    {
        public int? step { get; set; }
        public string? step_title { get; set; }
        public string? description { get; set; }
        public bool? is_step_complete { get; set; }
    }
}
