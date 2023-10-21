using Dist.Sys.Lab2.Core;

namespace AuctionWebsite.ViewModels
{
    public class AuctionDetailsVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int StartingPrice { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string CreatedBy { get; set; }

        public List<BidVM> BidsVM { get; set; } = new();
        
        public static AuctionDetailsVM FromAuction(Auction auction)
        {
            var detailsVM = new AuctionDetailsVM()
            {
                Id = auction.Id,
                Name = auction.Name,
                Description = auction.Description,
                StartingPrice = auction.StartingPrice,
                ExpirationDate = auction.ExpirationDate,
                CreatedBy = auction.UserName
            };

            foreach(var b in auction.Bids)
            {
                detailsVM.BidsVM.Add(BidVM.FromBid(b));
            }

            return detailsVM;
        }
        

    }
}
