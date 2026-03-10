using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class InvestmentSellShare
    {
        public int Startupid { get; set; }
        public string Userid { get; set; } = null!;
        public int VirtualSharesAmount { get; set; }
        public decimal PricePerShare { get; set; }
        public DateTime SellDatetime { get; set; }
        public int? Id { get; set; }
    }
}
