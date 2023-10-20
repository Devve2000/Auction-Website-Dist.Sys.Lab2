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

        
        public Auction(int id, string name, string description, int startingPrice, DateTime expirationDate)
        {
            Id = id;
            Name = name;
            Description = description;
            StartingPrice = startingPrice;
            ExpirationDate = expirationDate;
        }
    }
}
