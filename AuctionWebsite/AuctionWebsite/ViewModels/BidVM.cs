
using Dist.Sys.Lab2.Core;

namespace AuctionWebsite.ViewModels
{
    public class BidVM
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        public string UserName { get; set; }

        public static BidVM FromBid(Bid bid)
        {
            return new BidVM()
            {
                Id = bid.Id,
                Amount = bid.Amount,
                Date = bid.Date,
                UserName = bid.UserName
            };
        }
    }
}
