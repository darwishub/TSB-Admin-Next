using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public class Users
    {
        [Key]
        public string UserId { get; set; } = null!;
        //public string Email { get; set; } = null!;
        //public string LoginProvider { get; set; } = null!;
        //public string ProviderKey { get; set; } = null!;
        //public string DisplayName { get; set; } = null!;
        //public DateTime JoinDate { get; set; }
        //public string Source { get; set; } = null!;
        //public string Country { get; set; } = null!;
        //public string City { get; set; } = null!;
        //public string? RefreshToken { get; set; }
        //public string? Photo { get; set; }
        public string? Photo { get; set; }
        public int? TsbCoin { get; set; }
        
    }

    public class Photos
    {
        public IList<Users>? photo { get; set; }
    }
}
