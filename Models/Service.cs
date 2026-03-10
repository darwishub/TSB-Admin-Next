using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class Service
    {
        public int Serviceid { get; set; }
        public string Userid { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public short Discount { get; set; }
        public string Category { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public decimal Investeurprice { get; set; }
        public int Missionid { get; set; }
        public int Providerid { get; set; }
        public bool Isapproved { get; set; }
        public int Startupid { get; set; }
        public int Views { get; set; }
        public int Clicks { get; set; }
        public DateTime Createdat { get; set; }
    }
}
