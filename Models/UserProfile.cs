namespace TheStartupBuddyV3.Models
{
    public partial class UserProfile
    {
        public int Profileid { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public byte[] Photo { get; set; } = null!;
        public string Communication { get; set; } = null!;
        public string Remarks { get; set; } = null!;
        public string Userid { get; set; } = null!;
        public short Status { get; set; }
        public string Title { get; set; } = null!;
        public string Linkedin { get; set; } = null!;
        public string Joinedas { get; set; } = null!;
        public bool Mentoringstatus { get; set; }
        public bool Investorstatus { get; set; }
        public bool Ispaidmentor { get; set; }
        public string Expertise { get; set; } = null!;
        public decimal Mentorprice { get; set; }
        public DateTime Createdat { get; set; }
        public int? BenefitTicket { get; set; }
        public int? LastInvoice { get; set; }
    }

    public partial class NetworkProfile {
        public int StartupId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PhotoProfile { get; set; }
        public string? JoinedAs { get; set;}
        public int? NetworkingWith { get; set; }
        public bool? ConnectionStatus { get; set; }
        public int? PriceConnect { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? AcceptDate { get; set; }
        public string? LinkedinAccount { get; set; }
        public string? EmailAccount { get; set; }
        public int? RequestBy { get; set; }
        public int? ProgramId { get; set; }
        public bool? UserOnBoardingStatus { get; set; }
        public string? MyExpertise { get; set; }
        public string? NetworkWithExpertiseAt { get; set; }
        public string? MyRole { get; set; }
        public string? NetworkWithRoles { get; set; }
        public string? Gmail { get; set; }
    }

    public class onBoardPersonalInfo
    {
        public bool? is_step_complete { get; set; }
        public Array? cards { get; set; }
    }
}
