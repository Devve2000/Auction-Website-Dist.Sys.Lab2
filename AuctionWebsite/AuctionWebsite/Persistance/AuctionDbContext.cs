using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AuctionWebsite.Persistance
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }

        public DbSet<AuctionDb> AuctionDbs { get; set; }
        public DbSet<BidDb> BidDbs { get; set; }

    }
}
