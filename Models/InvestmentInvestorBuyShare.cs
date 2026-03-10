using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class InvestmentInvestorBuyShare
    {
        public int BuyId { get; set; }
        public int StartupSharesOwnerId { get; set; }
        public string InvestorUserid { get; set; } = null!;
        public string InvestorEmail { get; set; } = null!;
        public int AmountSharesBought { get; set; }
        public decimal CurrentPricePerShare { get; set; }
        public decimal TotalCurrentSharesPrice { get; set; }
        public DateTime BuyDatetime { get; set; }
    }
}
