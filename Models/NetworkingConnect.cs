using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public class NetworkingConnect
    {
        [Key]
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? AccepDate { get; set; }
        public bool ConnectionStatus { get; set; }
        public int? RequestBy { get; set; }
    }
}
