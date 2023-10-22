using AuctionWebsite.Persistance;
using AutoMapper;
using Dist.Sys.Lab2.Core;

namespace AuctionWebsite.MapperProfiles
{
    public class BidProfile : Profile
    {
        public BidProfile() 
        {
            CreateMap<BidDb, Bid>().ReverseMap();
        }
    }
}
