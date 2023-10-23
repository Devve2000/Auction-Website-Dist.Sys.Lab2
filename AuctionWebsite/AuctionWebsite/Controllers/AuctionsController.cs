using AuctionWebsite.ViewModels;
using Dist.Sys.Lab2.Core.Interfaces;
using Dist.Sys.Lab2.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;

namespace AuctionWebsite.Controllers
{

    [Authorize]
    public class AuctionsController : Controller
    {
        private readonly IAuctionService _auctionService;

        public AuctionsController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        // GET: AuctionsController
        public ActionResult Index()
        {
            List<Auction> auctions = _auctionService.GetAll();
            List<AuctionVM> auctionVMs = new List<AuctionVM>();
            foreach (var auction in auctions)
            {
                auctionVMs.Add(AuctionVM.FromAuction(auction));
            }

            return View(auctionVMs);
        }

        // GET: AuctionsController
        public ActionResult BidAuctions()
        {
            List<Auction> auctions = _auctionService.GetBiddedAuctions(User.Identity.Name);
            List<AuctionVM> auctionVMs = new List<AuctionVM>();
            foreach (var auction in auctions)
            {
                auctionVMs.Add(AuctionVM.FromAuction(auction));
            }

            return View(auctionVMs);
        }

        // GET: AuctionsController
        public ActionResult WonAuctions()
        {
            List<Auction> auctions = _auctionService.GetWonAuctions(User.Identity.Name);
            List<AuctionVM> auctionVMs = new List<AuctionVM>();
            foreach (var auction in auctions)
            {
                auctionVMs.Add(AuctionVM.FromAuction(auction));
            }

            return View(auctionVMs);
        }


        
        // GET: AuctionsController/Details/5
        public ActionResult Details(int id)
        {
            Auction a = _auctionService.GetAuctionById(id);
            if (a == null) return NotFound();
            AuctionDetailsVM advm = AuctionDetailsVM.FromAuction(a);
            return View(advm);
        }


        
        // GET: AuctionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuctionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuctionCreateVM acvm)
        {
            if (ModelState.IsValid)
            {
                Auction a = new Auction()
                {
                    Name = acvm.Name,
                    Description = acvm.Description,
                    StartingPrice = acvm.StartingPrice,
                    ExpirationDate = acvm.ExpirationDate,
                    UserName = User.Identity.Name
                };
                _auctionService.Add(a);
                return RedirectToAction("Index");
            }
            return View();
        }

        
        // GET: AuctionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return checkAuthorizationOfResource(id);
        }

        // POST: AuctionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AuctionUpdateVM auvm)
        {
            if (ModelState.IsValid)
            {
                Auction a = new Auction()
                {
                    Id = id,
                    Description = auvm.Description
                };
                _auctionService.Update(a);
                return RedirectToAction("Index");
            }
            return View();
        }


        // GET: AuctionsController/CreateBid
        public ActionResult CreateBid(int id)
        {
            if (id < 1) return NotFound();
            Auction a = _auctionService.GetOnlyAuctionInfoById(id);
            if (a == null) return NotFound();

            if (a.UserName.Equals(User.Identity.Name))
            {
                return RedirectToAction("Details", new { id = a.Id });
            }
            return View();
        }

        // POST: AuctionsController/CreateBid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBid(int id, BidCreateVM bcvm)
        {
            if (ModelState.IsValid)
            {
                Auction a = _auctionService.GetAuctionById(id);

                int highestBid = a.highestBid();

                if(bcvm.Bid > highestBid && bcvm.Bid >= a.StartingPrice && a.ExpirationDate > DateTime.Now)
                {
                    Bid newBid = Bid.createNewBid(bcvm.Bid, User.Identity.Name);
                    int auctionId = id;
                    _auctionService.AddBidToAuction(id, newBid);
                    return RedirectToAction("Details", new { id = auctionId});
                }
                else
                {
                    if (a.ExpirationDate <= DateTime.Now) ModelState.AddModelError("BidError", "Auction has expired, can't place bid");
                    else if (bcvm.Bid < highestBid) ModelState.AddModelError("BidError", "To low of a bid, needs to be atleast above the last bid of :" + highestBid + " kr");
                    else ModelState.AddModelError("BidError", "To low of a bid, needs to be atleast the starting price of " + a.StartingPrice + " kr");
                    return View();
                }
            }
            return View();
        }


        // GET: AuctionsController/DetailsOfWonAuction/5
        public ActionResult DetailsOfWonAuction(int id)
        {
            Auction a = _auctionService.GetAuctionById(id);
            if (a == null) return NotFound();

            //Check if the highest bidder
            if(a.isWinner(User.Identity.Name))
            {
                AuctionDetailsVM advm = AuctionDetailsVM.FromAuction(a);
                return View(advm);
            }
            else
            {
                return NotFound();
            }
            
        }


        /*
         * Private help method for authenication check.
         * 
         * param: id - The id of a resource to check authority.
         * returns: ActionResult that should execute.
         * **/
        private ActionResult checkAuthorizationOfResource(int id)
        {
            if (id < 1) return NotFound();
            Auction a = _auctionService.GetAuctionById(id);
            if (a == null) return NotFound();
            if (a.UserName.Equals(User.Identity.Name))
            {
                return View();
            }
            return BadRequest();
        }
    }
}
