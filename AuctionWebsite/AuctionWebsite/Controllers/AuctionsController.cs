using AuctionWebsite.ViewModels;
using Dist.Sys.Lab2.Core.Interfaces;
using Dist.Sys.Lab2.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            // TODO: Check if the user owns the resourse
            return View();
        }

        // POST: AuctionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
    }
}
