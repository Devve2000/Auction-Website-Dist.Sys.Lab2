namespace Dist.Sys.Lab2.Core.Interfaces
{
    public interface IAuctionPersistence
    {
        List<Auction> GetAll();

        Auction GetAuctionById(int id);

        void Add(Auction auction);
    }
}
