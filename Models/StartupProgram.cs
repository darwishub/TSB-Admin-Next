using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStartupBuddyV3.Models
{
    public partial class StartupProgram
    {
        [Key, Column(Order = 0)]
        public int ProgramId { get; set; }
        [Key, Column(Order = 1)]
        public int StartupId { get; set; }
        [Key, Column(Order = 2)]
        public DateTime JoinDate { get; set; }
        public int ProgramGroupId { get; set; }
    }

    public class ProgramStatus
    {
        public int ProgramId { get; set; }
        public bool? StatusGoal { get; set; }
        public string ProgramName { get; set; }
        public int? ProgramGroupId { get; set; }
    }
}
