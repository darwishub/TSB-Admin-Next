using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class UserFieldFile
    {
        public string Userid { get; set; } = null!;
        public int Fieldid { get; set; }
        public string Filename { get; set; } = null!;
        public byte[]? Data { get; set; }
        public int Fileid { get; set; }
        public string Type { get; set; } = null!;
        public int Startupid { get; set; }
        public DateTime Lastupdated { get; set; }
    }
}
