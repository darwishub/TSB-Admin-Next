using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public class Step
    {
        [Key]
        public int IdStep { get; set; }
        public string TitleStep { get; set; } = null!;
        public DateTime? CreatedDateStep { get; set; }
        public string? DescriptionStep { get; set; }
    }
}
