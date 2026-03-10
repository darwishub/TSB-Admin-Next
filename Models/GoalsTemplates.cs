using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public class GoalsTemplates
    {
        [Key]
        public int id_template { get; set; }
        public string? photo_template { get; set; }
        public int? id_level { get; set; }
        public byte? status { get; set; }
    }
}
