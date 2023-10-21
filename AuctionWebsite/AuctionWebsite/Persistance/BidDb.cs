using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionWebsite.Persistance
{
    public class BidDb
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        //Foreign key to the Auction
        [Required]
        [ForeignKey("AuctionId")]
        public AuctionDb AuctionDb { get; set; }
        public int AuctionId { get; set; }

        [Required]
        
        //Owner of bid
        public string UserName { get; set; }
    }
}
