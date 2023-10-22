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

        public void Update(Auction auction)
        {
            _auctionPersistance.Update(auction);
        }

        public Auction GetOnlyAuctionInfoById(int id)
        {
            return _auctionPersistance.GetOnlyAuctionInfoById(id);
        }

        public List<Bid> GetBidsOfAuctionById(int id)
        {
            return _auctionPersistance.GetBidsOfAuctionById(id);
        }

        public void AddBidToAuction(int auctionId, Bid bid)
        {
            _auctionPersistance.AddBidToAuction(auctionId, bid);
        }

        public List<Auction> GetBiddedAuctions(string userName)
        {
            return _auctionPersistance.GetBiddedAuctions(userName);
        }
        public List<Auction> GetWonAuctions(string userName)
        {
            return _auctionPersistance.GetWonAuctions(userName);
        }




    }
}
