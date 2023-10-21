using Dist.Sys.Lab2.Core;
using Dist.Sys.Lab2.Core.Interfaces;

namespace AuctionWebsite.Core
{
    public class AuctionService : IAuctionService
    {
        private IAuctionPersistence _auctionPersistance;

        public AuctionService(IAuctionPersistence auctionPersistence) 
        {
            _auctionPersistance = auctionPersistence;
        }

        public List<Auction> GetAll()
        {
            return _auctionPersistance.GetAll();
        }

        public Auction GetAuctionById(int id)
        {
            return _auctionPersistance.GetAuctionById(id);
        }

        public void Add(Auction auction)
        {
            _auctionPersistance.Add(auction);
        }
    }
}
