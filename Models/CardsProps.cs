namespace TheStartupBuddyV3.Models
{
    public class CardsProps
    {
        public string? placeholder { get; set; }
        public string? type { get; set; }
        public string? type_rule { get; set; }
        public string? label { get; set; }
        public string? value { get; set; }
        public byte? required { get; set; }
        public byte? disabled { get; set; }
        public int? max_data { get; set; }
        public byte? allow_multiple { get; set; }
        public string? labelOption { get; set; }
        public string? valueOption { get; set; }
    }

    public class Props
    {
        public string? placeholder { get; set; }
        public string? type { get; set; }
        public string? type_rule { get; set; }
        public string? label { get; set; }
        public string? value { get; set; }
        public bool? required { get; set; }
        public bool? disabled { get; set; }
        public int? max_data { get; set; }
        public bool? allow_multiple { get; set; }
    }

    public class Schema
    {
        public int? field_code { get; set; }
        public int? field_code_type { get; set; }
        //public IList<Props> props { get; set; }
        public Props? props { get; set; }
        public bool? has_option { get; set; }
        //public IList<Option> options { get; set; }
        public int? display_order { get; set; }
        public string? success_message { get; set; }
        public string? options { get; set; }
    }
}
