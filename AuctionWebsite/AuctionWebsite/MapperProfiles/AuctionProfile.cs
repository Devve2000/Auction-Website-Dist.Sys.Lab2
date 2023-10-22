using AuctionWebsite.Persistance;
using AutoMapper;
using Dist.Sys.Lab2.Core;

namespace AuctionWebsite.MapperProfiles
{
    public class AuctionProfile : Profile
    {
        public AuctionProfile() 
        {
            CreateMap<AuctionDb, Auction>().ReverseMap();   
        }
    }
}
