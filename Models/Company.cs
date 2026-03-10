using System;
using System.Collections;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class Company
    {
        public int Companyid { get; set; }
        public string Userid { get; set; } = null!;
        public string Companyname { get; set; } = null!;
        public string Website { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[]? Logo { get; set; }
    }

}
