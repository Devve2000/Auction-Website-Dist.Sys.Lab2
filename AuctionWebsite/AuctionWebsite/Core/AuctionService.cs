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
            if (auction != null || auction.Id != 0) throw new InvalidDataException("Auction is null or id has been changed.");
            _auctionPersistance.Add(auction);
        }

        public void Update(Auction auction)
        {
            if (auction == null) throw new InvalidDataException("Auction is null.");
            _auctionPersistance.Update(auction);
        }

        public Auction GetOnlyAuctionInfoById(int id)
        {
            if(id  < 1) throw new ArgumentOutOfRangeException("id");
            return _auctionPersistance.GetOnlyAuctionInfoById(id);
        }

        public List<Bid> GetBidsOfAuctionById(int id)
        {
            if(id < 1) throw new ArgumentOutOfRangeException("id");
            return _auctionPersistance.GetBidsOfAuctionById(id);
        }

        public void AddBidToAuction(int auctionId, Bid bid)
        {
            if(auctionId < 1) throw new ArgumentOutOfRangeException("id");
            if (bid == null) throw new ArgumentNullException("bid");
            _auctionPersistance.AddBidToAuction(auctionId, bid);
        }

        public List<Auction> GetBiddedAuctions(string userName)
        {
            if (userName == null) throw new ArgumentNullException("userName");
            return _auctionPersistance.GetBiddedAuctions(userName);
        }
        public List<Auction> GetWonAuctions(string userName)
        {
            if (userName == null) throw new ArgumentNullException("userName");
            return _auctionPersistance.GetWonAuctions(userName);
        }




    }
}
