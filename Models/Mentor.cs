using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class Mentor
    {
        public int Mentorid { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public byte[] Photo { get; set; } = null!;
        public string Communication { get; set; } = null!;
        public string Remarks { get; set; } = null!;
        public string Userid { get; set; } = null!;
        public short Status { get; set; }
        public string Title { get; set; } = null!;
    }
}
