using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public partial class ProgramGroup
    {
        [Key]
        public int ProgramGroupId { get; set; } 
        public int Programid { get; set; } 
        public string? GroupName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
