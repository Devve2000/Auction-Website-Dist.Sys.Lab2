using System.ComponentModel.DataAnnotations;


namespace AuctionWebsite.ViewModels
{
    public class AuctionUpdateVM
    {
        [Required]
        [StringLength(256, ErrorMessage = "Max length 256 characters")]
        public string Description { get; set; }
    }
}
