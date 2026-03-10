using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class InvestmentInvestorOfferShare
    {
        public int OfferId { get; set; }
        public string InvestorEmail { get; set; } = null!;
        public string InvestorUserid { get; set; } = null!;
        public int OfferToStartupid { get; set; }
        public int ShareAmountOffer { get; set; }
        public decimal PricePerShareOffer { get; set; }
        public decimal TotalShareOffer { get; set; }
        public DateTime TimeOffer { get; set; }
    }
}
