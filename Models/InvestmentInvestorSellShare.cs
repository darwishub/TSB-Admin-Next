using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class InvestmentInvestorSellShare
    {
        public int SellId { get; set; }
        public string InvestorEmail { get; set; } = null!;
        public string InvestorUserid { get; set; } = null!;
        public int SellToStartupid { get; set; }
        public int ShareAmountSell { get; set; }
        public decimal PricePerShareSell { get; set; }
        public decimal TotalShareSell { get; set; }
        public DateTime TimeSell { get; set; }
    }
}
