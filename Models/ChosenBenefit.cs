using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class ChosenBenefit
    {
        public int ChosenId { get; set; }
        public string BenefitOwnerUserid { get; set; } = null!;
        public int BenefitChosenId { get; set; }
        public DateTime BenefitDate { get; set; }
        public int BenefitOwnerStartupid { get; set; }
    }
}
