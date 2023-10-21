using Dist.Sys.Lab2.Core;

namespace AuctionWebsite.ViewModels
{
    public class AuctionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StartingPrice  { get; set; }
        public DateTime ExpirationDate { get; set; }

        //Lista med bids?

        public static AuctionVM FromAuction(Auction auction)
        {
            return new AuctionVM
            {
                Id = auction.Id,
                Name = auction.Name,
                Description = auction.Description,
                StartingPrice = auction.StartingPrice,
                ExpirationDate = auction.ExpirationDate
            };
        }
    }
}
