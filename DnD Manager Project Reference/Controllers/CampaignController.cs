using DnDManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace DnDManager.Controllers
{
    public class CampaignController : Controller
    {
        private readonly AppDbContext _context;

        public CampaignController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Campaign
        public IActionResult Index()
        {
            List<Campaign> campaigns = _context.Campaigns.ToList();
            return View(campaigns);
        }

        // GET: /Campaign/Details/1
        public IActionResult Details(int id)
        {
            Campaign? campaign = _context.Campaigns
                .Where(c => c.CampaignId == id)
                .FirstOrDefault();

            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // GET: /Campaign/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Campaign/Create
        [HttpPost]
        public IActionResult Create([Bind("Name,Description")] Campaign campaign)
        {
            if (!ModelState.IsValid)
            {
                return View(campaign);
            }

            _context.Campaigns.Add(campaign);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: /Campaign/Edit/1
        public IActionResult Edit(int id)
        {
            Campaign? campaign = _context.Campaigns.Find(id);

            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // POST: /Campaign/Edit/1
        [HttpPost]
        public IActionResult Edit([Bind("CampaignId,Name,Description")] Campaign campaign)
        {
            if (!ModelState.IsValid)
            {
                return View(campaign);
            }

            _context.Campaigns.Update(campaign);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: /Campaign/Delete/1
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Campaign? campaign = _context.Campaigns.Find(id);

            if (campaign != null)
            {
                _context.Campaigns.Remove(campaign);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}