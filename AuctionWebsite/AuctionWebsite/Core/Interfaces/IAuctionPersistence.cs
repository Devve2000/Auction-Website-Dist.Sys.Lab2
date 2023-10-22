namespace Dist.Sys.Lab2.Core.Interfaces
{
    public interface IAuctionPersistence
    {
        List<Auction> GetAll();

        Auction GetAuctionById(int id);

        void Add(Auction auction);

        void Update(Auction auction);

        List<Auction> GetBiddedAuctions(string userName);

        List<Auction> GetWonAuctions(string userName);
    }
}
