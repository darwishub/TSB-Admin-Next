using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public partial class User
    {
        public User()
        {
            ClicksDataLogs = new HashSet<ClicksDataLog>();
        }
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string LoginProvider { get; set; } = null!;
        public string ProviderKey { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public DateTime JoinDate { get; set; }
        public string Source { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public string? Photo { get; set; }
        public bool? OnBoardingClicked { get; set; }
        public bool? UserOnBoardingStatus { get; set; }
        public virtual ICollection<ClicksDataLog> ClicksDataLogs { get; set; }
    }

    

}
