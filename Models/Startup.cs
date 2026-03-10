using Stripe;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class Startup
    {
        public Startup()
        {
            ClicksDataLogs = new HashSet<ClicksDataLog>();
            FavouritedDataLogs = new HashSet<FavouritedDataLog>();
        }

        public int Startupid { get; set; }
        public string Userid { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Website { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Publicprofilevisible { get; set; }
        public byte[]? Logo { get; set; }
        public string Startupstatus { get; set; } = null!;
        public int Clicks { get; set; }
        public bool Ispublic { get; set; }
        public int Virtualshares { get; set; }
        public byte DoubleShares { get; set; }
        public string? PitchUrl { get; set; }
        public string? VideoUrl { get; set; }
        public decimal? CurrentFund { get; set; }
        public decimal? TotalFund { get; set; }
        public int? BenefitTicket { get; set; }
        public int? LastInvoice { get; set; }
        public int? tsbCoin { get; set; }
        public int? priceConnect { get; set; }
        public string? startupLogo { get; set; }
        public virtual ICollection<ClicksDataLog> ClicksDataLogs { get; set; }
        public virtual ICollection<FavouritedDataLog> FavouritedDataLogs { get; set; }
        public int? startupScore { get; set; }
        public string? startupNote { get; set; }
    }

    public class StartupCompany
    {
        public int startupid { get; set; }
        public string? companyname { get; set; }
        public string? companyEmail { get; set; }
        public string? companyDescription { get; set; }
        public ArrayList? memberphoto { get; set; }
        public ArrayList? levels { get; set; }
        public int? level { get; set; }
        public int? tsbCoin { get; set; }
        public int? steps { get; set; }
        public int? totalStep { get; set; }
        public string? ranking { get; set; }
        public int? totalLeaderBoard { get; set; }
        public string? program { get; set; }
        public string? startupLogo { get; set; }
        public int? Programid { get; set; }
        public int? ProgramGroupId { get; set; }
        public int? Score { get; set; }
        public DateTime? dateCreated { get; set; }
        public List<ProgramGroup> ProgramGroup { get; set; }
        public List<MembersOfStartup> members { get; set; }
    }

    public class StartupCompanyWithRanking
    {
        public int startupid { get; set; }
        public string? companyname { get; set; }
        public string? companyEmail { get; set; }
        public string? description { get; set; }
        public ArrayList? memberphoto { get; set; }
        public ArrayList? level { get; set; }
        public int? tsbCoin { get; set; }
        public int? steps { get; set; }
        public int? totalStep { get; set; }
        public int? ranking { get; set; }
        public int? totalLeaderBoard { get; set; }
        public string? program { get; set; }
        public string? startupLogo { get; set; }
        public int? Programid { get; set; }
        public int? ProgramGroupId { get; set; }
        public List<ProgramGroup> programGroups { get; set; }
        public int? Score { get; set; }
        public DateTime? dateCreated { get; set; }
        public int? completeStep { get; set; }
        public int? totalSteps { get; set; }
    }

    public partial class CurrentStartupLevelPerCategory
    {
        public int? id_level { get; set; }
        public int? id_goal_category { get; set; }
        public int? code_level { get; set; }
        public string? logo_url { get; set; }
    }

    public class StartupInfo
    {
        public int startupid { get; set; }
        public string? companyname { get; set; }
        public string? companyEmail { get; set; }
        public string? companyDescription { get; set; }
        public int? tsbCoin { get; set; }
        public string? website { get; set; }
        public int? priceConnect { get; set; }
        public string? startupLogo { get; set; }
        public DateTime? joinDate { get; set; }
        public string? country { get; set; }
        public string? ownerPhoto { get; set; }
        public bool? onBoardingStatus { get; set; }
        public string? ownerName { get; set; }
        public string? linkedin { get; set; }
        public List<MembersOfStartup>? members { get; set; }
    }

    public class AllUsers
    {
        public int startupid { get; set; }
        public string? companyname { get; set; }
        public string? email { get; set; }
        public string? userid { get; set; }
        public string? JoinedAs { get; set; }
    }

    public class SaveNoteRequest
    {
        public string? Note { get; set; }
    }
}
