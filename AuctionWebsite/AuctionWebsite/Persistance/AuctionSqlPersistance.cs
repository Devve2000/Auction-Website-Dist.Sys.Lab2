using AutoMapper;
using Dist.Sys.Lab2.Core;
using Dist.Sys.Lab2.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuctionWebsite.Persistance
{
    public class AuctionSqlPersistance : IAuctionPersistence
    {
        private AuctionDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuctionSqlPersistance(AuctionDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<Auction> GetAll()
        {
            //Fixa använda användarhantering!
            var AuctionDbs = _dbContext.AuctionDbs
                .Where(a => a.ExpirationDate > DateTime.Now)
                //.Include(p => p.BidDbs)
                .OrderByDescending(a => a.ExpirationDate)
                .ToList();

            List<Auction> result = new List<Auction>();
            foreach(AuctionDb adb  in AuctionDbs)
            {
                Auction auction = new Auction(
                    adb.Id,
                    adb.Name,
                    adb.Description,
                    adb.StartingPrice,
                    adb.ExpirationDate,
                    adb.UserName);

                result.Add( auction );
            }

            return result;
        }

        public Auction GetAuctionById(int id)
        {
            //Fixa använda användarhantering!
            var auctionDb = _dbContext.AuctionDbs
                .Where(a => a.Id == id)
                .Include(a => a.BidDbs)
                .SingleOrDefault();

            Auction auction = _mapper.Map<Auction>(auctionDb);

            /*
            Auction auction = new Auction(
                auctionDb.Id,
                auctionDb.Name,
                auctionDb.Description,
                auctionDb.StartingPrice,
                auctionDb.ExpirationDate,
                auctionDb.UserName);
            */

            foreach(BidDb bdb in auctionDb.BidDbs)
            {
                //auction.addBid(new Bid(bdb.Id, bdb.Amount, bdb.Date, bdb.UserName));
                auction.addBid(_mapper.Map<Bid>(bdb));
            }
            return auction;
        }

        public void Add(Auction a)
        {
            AuctionDb adb = new AuctionDb()
            {
                Name = a.Name,
                Description = a.Description,
                StartingPrice = a.StartingPrice,
                ExpirationDate = a.ExpirationDate,
                UserName = a.UserName
            };
            _dbContext.AuctionDbs.Add(adb);
            _dbContext.SaveChanges();
        }

        //Krav 3
        //Uppdatera auction. Antingen (Auction a) eller (int id, string description) ?
        public void Update(int id, string newDescription)
        {
            var auctionDb = _dbContext.AuctionDbs
                .Where(a => a.Id == id)
                .SingleOrDefault();

            if(auctionDb != null)
            {
                auctionDb.Description = newDescription;

                _dbContext.SaveChanges();
            }

        }

        // Krav 7
        public List<Auction> GetBiddedAuctions(string userName)
        {
            // Any returnerar true ifall en auction har en bid gjord av userName
            var auctionDbs = _dbContext.AuctionDbs
                .Include(a => a.BidDbs)
                .Where(a => a.BidDbs.Any(b => b.UserName == userName) && a.ExpirationDate > DateTime.Now)
                .ToList();

            List<Auction> result = new List<Auction>();
            foreach (AuctionDb adb in auctionDbs)
            {
                Auction auction = new Auction(
                    adb.Id,
                    adb.Name,
                    adb.Description,
                    adb.StartingPrice,
                    adb.ExpirationDate,
                    adb.UserName);

                result.Add(auction);
            }

            return result;
        }

        // Krav 8
        public List<Auction> GetWonAuctions(string userName)
        {
            var auctionDbs = _dbContext.AuctionDbs
                .Include(a => a.BidDbs)
                .Where(a => a.BidDbs.Any(b => b.UserName == userName) &&                        // 1. Hämta alla autions som har bids av användaren
                       a.ExpirationDate < DateTime.Now &&                                       // 2. Filtrera bort pågående auktioner
                       a.BidDbs.OrderByDescending(b => b.Amount).First().UserName == userName)  // 3. Sätt högsta talet först, sen kolla ifall budet är av användaren
                .ToList();

            List<Auction> result = new List<Auction>();
            foreach (AuctionDb adb in auctionDbs)
            {
                Auction auction = new Auction(
                    adb.Id,
                    adb.Name,
                    adb.Description,
                    adb.StartingPrice,
                    adb.ExpirationDate,
                    adb.UserName);

                result.Add(auction);
            }

            return result;
        }
    }
}
