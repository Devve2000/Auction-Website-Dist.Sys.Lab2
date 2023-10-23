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

        public Bid(int amount, DateTime date, string userName)
        {
            Amount = amount;
            Date = date;
            UserName = userName;
        }

        public static Bid createNewBid(int amount, string userName)
        {
            return new Bid(amount, DateTime.Now, userName);
        }


    }
}
