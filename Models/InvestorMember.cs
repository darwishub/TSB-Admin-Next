using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class InvestorMember
    {
        public int IdInvestorMember { get; set; }
        public string UseridTeamOwner { get; set; } = null!;
        public string MemberEmail { get; set; } = null!;
        public short Status { get; set; }
        public string? MemberName { get; set; }
    }
}
