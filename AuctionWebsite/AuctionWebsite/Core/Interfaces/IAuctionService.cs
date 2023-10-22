namespace Dist.Sys.Lab2.Core.Interfaces
{
    public interface IAuctionService
    {
        List<Auction> GetAll();
        
        void Add(Auction auction);


        Auction GetAuctionById(int id);

        void Update(Auction auction);

        Auction GetOnlyAuctionInfoById(int id);

        List<Bid> GetBidsOfAuctionById(int id);

        void AddBidToAuction(int auctionId, Bid bid);

        List<Auction> GetBiddedAuctions(string userName);

        List<Auction> GetWonAuctions(string userName);

    }
}
