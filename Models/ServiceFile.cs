using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class ServiceFile
    {
        public int Fileid { get; set; }
        public int Serviceid { get; set; }
        public byte[]? Data { get; set; }
        public string Type { get; set; } = null!;
    }
}
