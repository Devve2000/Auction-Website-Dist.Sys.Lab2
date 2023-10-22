using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace AuctionWebsite.ViewModels
{
    public class BidCreateVM
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage ="Invalid bid")]
        public int Bid { get; set; }
    }
}
