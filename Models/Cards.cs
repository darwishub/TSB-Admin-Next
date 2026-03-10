using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public class Cards
    {
        [Key]
        public int id_card { get; set; }
        public int? id_step { get; set; }
        public string? help_text { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public int? field_code { get; set; }
        public int? field_code_type { get; set; }
        public string? placeholder { get; set; }
        public string? text_button { get; set; }
        public string? type { get; set; }
        public string? type_rule { get; set; }
        public string? label { get; set; }
        public string? value { get; set; }
        public bool? required { get; set; }
        public bool? disabled { get; set; }
        public int? max_data { get; set; }
        public bool? allow_multiple { get; set; }
        public byte? display_order { get; set; }
        public string? success_message { get; set; }
        public bool? has_option { get; set; }
        public string? options { get; set; }
    }
}
