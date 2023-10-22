using AuctionWebsite.Persistance;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dist.Sys.Lab2.Core
{
    public class Bid
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        public string UserName { get; set; }

        public Bid(int id, int amount, DateTime date, string userName) 
        {
            Id = id;
            Amount = amount;
            Date = date;
            UserName = userName;
        }

        public Bid() { }

        /*
        public Bid getNewBid(int bidAmount, string userName, int highestBid, int startingPrice)
        {
            if(bidAmount > highestBid && bidAmount >= startingPrice) 
            {
                return new Bid()
                {
                    Amount = bidAmount,
                    Date = DateTime.Now,
                    UserName = userName
                };
            }

            return null;
        }

        */
    }
}
