using AuctionWebsite.ViewModels;
using Dist.Sys.Lab2.Core.Interfaces;
using Dist.Sys.Lab2.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            List<Auction> auctions = _auctionService.GetAll();
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
            List<Auction> auctions = _auctionService.GetAll();
            List<AuctionVM> auctionVMs = new List<AuctionVM>();
            foreach (var auction in auctions)
            {
                auctionVMs.Add(AuctionVM.FromAuction(auction));
            }

            return View(auctionVMs);
        }

        // GET: AuctionsController
        public ActionResult CreateAuction()
        {
            return View();
        }

        
        // GET: AuctionsController/Details/5
        public ActionResult Details(int id)
        {
            if (id == null) throw new ArgumentNullException(); //************Ändra till "not found"
            Auction a = _auctionService.GetAuctionById(id);
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


        // GET: AuctionsController/Create
        public ActionResult CreateBid(int id)
        {
            Auction a = _auctionService.GetAuctionById(id);
            if (a == null) return NotFound();

            if (a.UserName.Equals(User.Identity.Name)) return RedirectToAction("Details",id);
            
            
            return View();
        }

        // POST: AuctionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBid(int id, BidCreateVM bcvm)
        {
            if (ModelState.IsValid)
            {
                Auction a = _auctionService.GetAuctionById(id);
                if(bcvm.Bid > a.highestBid())
                {

                }
                else
                {
                    // Can't enter a bid less.
                    ModelState.AddModelError("BidError", "To low of a bid, go above the last one: " + a.highestBid());
                    return View();
                }
                // TODO : Skapa bid
                // TODO : Kalla service CreateBid
                // TODO : Returnera till index sidan.
            }
            return View();
        }

        /*
        // GET: AuctionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuctionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        */

        private ActionResult checkAuthorizationOfResource(int id)
        {
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
