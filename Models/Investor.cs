using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class Investor
    {
        public string UserId { get; set; } = null!;
        public string Investortype { get; set; } = null!;
        public string Vcname { get; set; } = null!;
        public string Marketfocus { get; set; } = null!;
        public string Investmentregions { get; set; } = null!;
        public string Investmentcategories { get; set; } = null!;
        public int Minimumrevenue { get; set; }
        public int MinInvestment { get; set; }
        public int MaxInvestment { get; set; }
        public bool Isapproved { get; set; }
        public string? Invesmentstage { get; set; }
        public byte? Invesmentdeclare { get; set; }
        public decimal Virtualmoney { get; set; }
        public byte DoubleMoney { get; set; }
    }
}
