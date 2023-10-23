using AutoMapper;
using Dist.Sys.Lab2.Core;
using Dist.Sys.Lab2.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

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
            var AuctionDbs = _dbContext.AuctionDbs
                .Where(a => a.ExpirationDate > DateTime.Now)
                .OrderByDescending(a => a.ExpirationDate)
                .ToList();

            List<Auction> result = new List<Auction>();
            foreach(AuctionDb adb  in AuctionDbs)
            {
                Auction auction = _mapper.Map<Auction>(adb);
                result.Add( auction );
            }

            return result;
        }

        public Auction GetAuctionById(int id)
        {

            var auctionDb = _dbContext.AuctionDbs
                .Where(a => a.Id == id)
                .Include(a => a.BidDbs.OrderByDescending(b => b.Amount))
                .SingleOrDefault();



            Auction auction = _mapper.Map<Auction>(auctionDb);

            if (auctionDb != null) 
                foreach(BidDb bdb in auctionDb.BidDbs)
                {
                    auction.addBid(_mapper.Map<Bid>(bdb));
                }
            
            return auction;
        }
        public Auction GetOnlyAuctionInfoById(int id)
        {

            var auctionDb = _dbContext.AuctionDbs
                .Where(a => a.Id == id)
                .SingleOrDefault();

            Auction auction = _mapper.Map<Auction>(auctionDb);
            return auction;
        }

        public List<Bid> GetBidsOfAuctionById(int id)
        {
            var BidsDb = _dbContext.BidDbs
                .Where(b => b.AuctionId == id)
                .ToList();

            List<Bid> bids = new List<Bid>();
            foreach(BidDb bdb in BidsDb)
            {
                bids.Add(_mapper.Map<Bid>(bdb));
            }
            return bids;
        }

        public void Add(Auction a)
        {
            AuctionDb adb = _mapper.Map<AuctionDb>(a);
            _dbContext.AuctionDbs.Add(adb);
            _dbContext.SaveChanges();
        }

        public void Update(Auction auction)
        {
            var auctionDb = _dbContext.AuctionDbs
                .Where(a => a.Id == auction.Id)
                .SingleOrDefault();

            if(auctionDb != null)
            {
                auctionDb.Description = auction.Description;

                _dbContext.SaveChanges();
            }

        }

        public void AddBidToAuction(int auctionId, Bid bid)
        {
            BidDb bdb = _mapper.Map<BidDb>(bid);
            bdb.AuctionId = auctionId;
            _dbContext.BidDbs.Add(bdb);
            _dbContext.SaveChanges();
        }


        public List<Auction> GetBiddedAuctions(string userName)
        {
            // Any returnerar true ifall en auction har en bid gjord av userName
            var auctionDbs = _dbContext.AuctionDbs
                .Where(a => a.BidDbs.Any(b => b.UserName == userName) && a.ExpirationDate > DateTime.Now)
                .ToList();

            List<Auction> result = new List<Auction>();
            foreach (AuctionDb adb in auctionDbs)
            {
                Auction auction = _mapper.Map<Auction>(adb);
                result.Add(auction);
            }

            return result;
        }

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
                Auction auction = _mapper.Map<Auction>(adb);
                result.Add(auction);
            }

            return result;
        }
    }
}
