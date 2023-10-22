using Microsoft.IdentityModel.Tokens;

namespace Dist.Sys.Lab2.Core
{
    public class Auction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StartingPrice { get; set; }
        public DateTime ExpirationDate { get; set; }

        private List<Bid> _bids = new List<Bid>();
        public IEnumerable<Bid> Bids => _bids;

        public string UserName { get; set; }
        
        public void addBid(Bid b)
        {
            if (b == null) throw new ArgumentException();

            _bids.Add(b);
        }

        public Auction(int id, string name, string description, int startingPrice, DateTime expirationDate, string userName)
        {
            Id = id;
            Name = name;
            Description = description;
            StartingPrice = startingPrice;
            ExpirationDate = expirationDate;
            UserName = userName;
        }

        public Auction()
        {
        }

        public int highestBid()
        {
            if (_bids.IsNullOrEmpty()) return 0;
            return _bids.MaxBy(b => b.Amount).Amount;
        }
    }
}
