using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public partial class CoinHistory
    {
        [Key]
        public int Id { get; set; }
        public int StartupId { get; set; }
        public int Amount { get; set; }
        public string TransactionType { get; set; } = null!;
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; } = null!;
    }

}
