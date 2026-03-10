using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class ServicePurchase
    {
        public int Purchaseid { get; set; }
        public string Userid { get; set; } = null!;
        public int Serviceid { get; set; }
        public decimal Price { get; set; }
        public string Email { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public DateTime Purchasetime { get; set; }
        public string Paymentid { get; set; } = null!;
    }
}
