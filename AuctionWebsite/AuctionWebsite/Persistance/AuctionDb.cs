using System.ComponentModel.DataAnnotations;

namespace AuctionWebsite.Persistance
{
    public class AuctionDb
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string Description { get; set; }

        [Required]
        public int StartingPrice { get; set; }

        // Connection to the UserDB
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationDate { get; set; }

        public IEnumerable<BidDb> BidDbs { get; set; } = new List<BidDb>();
    }
}
