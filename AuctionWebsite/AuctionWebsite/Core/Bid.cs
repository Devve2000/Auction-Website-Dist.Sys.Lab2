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

        public Bid(int id, int amount, DateTime date) 
        {
            Id = id;
            Amount = amount;
            Date = date;
        }
    }
}
