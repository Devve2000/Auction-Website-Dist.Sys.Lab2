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
    }
}
