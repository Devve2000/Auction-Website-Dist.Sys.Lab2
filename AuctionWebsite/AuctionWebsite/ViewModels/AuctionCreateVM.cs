using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Numerics;

namespace AuctionWebsite.ViewModels
{
    public class AuctionCreateVM
    {
        [Required]
        [StringLength(64, ErrorMessage = "Max length 64 characters")]
        public string Name { get; set; }


        [Required]
        [StringLength(256, ErrorMessage = "Max length 256 characters")]
        public string Description { get; set; }

        [Required]
        [Range(1,int.MaxValue)]
        public int StartingPrice { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

    }
}
