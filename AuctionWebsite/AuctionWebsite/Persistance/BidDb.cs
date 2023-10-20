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

        [Required]
        [ForeignKey("AuctionId")]
        public AuctionDb AuctionDb { get; set; }

        /* Vem som har lagt budet:
        [Required]
        [ForeignKey("UserId")]
        public UserDb UserDb { get; set; }
        */
    }
}
