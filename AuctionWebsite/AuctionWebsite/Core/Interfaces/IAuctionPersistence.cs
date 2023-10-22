namespace Dist.Sys.Lab2.Core.Interfaces
{
    public interface IAuctionPersistence
    {
        List<Auction> GetAll();

        Auction GetAuctionById(int id);

        void Add(Auction auction);

        void Update(int id, string newDescription);

        List<Auction> GetBiddedAuctions(string userName);

        List<Auction> GetWonAuctions(string userName);
    }
}
