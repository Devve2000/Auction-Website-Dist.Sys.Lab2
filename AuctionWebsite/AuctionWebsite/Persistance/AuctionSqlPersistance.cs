using Dist.Sys.Lab2.Core;
using Dist.Sys.Lab2.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuctionWebsite.Persistance
{
    public class AuctionSqlPersistance : IAuctionPersistence
    {
        private AuctionDbContext _dbContext;

        public AuctionSqlPersistance(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Auction> GetAll()
        {
            //Fixa använda användarhantering!
            var AuctionDbs = _dbContext.AuctionDbs
                //.Where(a => true)
                //.Include(p => p.BidDbs)
                .ToList();

            List<Auction> result = new List<Auction>();
            foreach(AuctionDb adb  in AuctionDbs)
            {
                Auction auction = new Auction(
                    adb.Id,
                    adb.Name,
                    adb.Description,
                    adb.StartingPrice,
                    adb.ExpirationDate);

                result.Add( auction );
            }

            return result;
        }
    }
}
