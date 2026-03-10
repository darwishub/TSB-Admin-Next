using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class AdminUser1
    {
        public AdminUser1()
        {
            Roles = new HashSet<Role>();
        }

        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Boolean IsActive { get; set; }
        public string? PlatformAccess { get; set; }
        public string? Role { get; set; }
        public Boolean TwoFactorStatus { get; set; }
        public byte? EmailNotification { get; set; }
        public int? ProgramId { get; set; }
        public string? Linkedin { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }
        public bool? OnBoardingClicked { get; set; }
        public bool? UserOnBoardingStatus { get; set; }
        public int? Coins { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
