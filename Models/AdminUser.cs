using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class AdminUser
    {
        public int Id { get; set; }
        /// <summary>
        /// &apos;&apos;
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// &apos;&apos;
        /// </summary>
        public string Password { get; set; } = null!;
        /// <summary>
        /// &apos;&apos;
        /// </summary>
        public string Role { get; set; } = null!;
        /// <summary>
        /// &apos;&apos;
        /// </summary>
        public string Platformaccess { get; set; } = null!;
    }
}
