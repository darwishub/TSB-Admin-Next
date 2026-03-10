using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public partial class ProgramCode
    {
        [Key]
        public int Programid { get; set; } 
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public int Limit { get; set; }
        public short? ProgramType { get; set; }
        public bool? Permission { get; set; }
        public bool? StatusGoal { get; set; }
    }
}
