namespace Dist.Sys.Lab2.Core.Interfaces
{
    public interface IAuctionService
    {
        List<Auction> GetAll();
        
        void Add(Auction auction);


        Auction GetAuctionById(int id);

        void Update(Auction auction);
    }
}
