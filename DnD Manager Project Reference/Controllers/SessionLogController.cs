using DnDManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace DnDManager.Controllers
{
    public class SessionLogController : Controller
    {
        private readonly AppDbContext _context;

        public SessionLogController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /SessionLog/Index?campaignId=1
        public IActionResult Index(int campaignId)
        {
            List<SessionLog> logs = _context.SessionLogs
                .Where(s => s.CampaignId == campaignId)
                .OrderByDescending(s => s.SessionDate)
                .ToList();

            ViewBag.CampaignId = campaignId;
            return View(logs);
        }

        // GET: /SessionLog/Details/1
        public IActionResult Details(int id)
        {
            SessionLog? log = _context.SessionLogs.Find(id);

            if (log == null)
            {
                return NotFound();
            }

            return View(log);
        }

        // GET: /SessionLog/Create?campaignId=1
        public IActionResult Create(int campaignId)
        {
            ViewBag.CampaignId = campaignId;
            return View();
        }

        // POST: /SessionLog/Create
        [HttpPost]
        public IActionResult Create([Bind("Title,SessionDate,Summary,Notes,CampaignId")] SessionLog log)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CampaignId = log.CampaignId;
                return View(log);
            }

            _context.SessionLogs.Add(log);
            _context.SaveChanges();

            return RedirectToAction("Index", new { campaignId = log.CampaignId });
        }

        // GET: /SessionLog/Edit/1
        public IActionResult Edit(int id)
        {
            SessionLog? log = _context.SessionLogs.Find(id);

            if (log == null)
            {
                return NotFound();
            }

            return View(log);
        }

        // POST: /SessionLog/Edit/1
        [HttpPost]
        public IActionResult Edit([Bind("SessionLogId,Title,SessionDate,Summary,Notes,CampaignId")] SessionLog log)
        {
            if (!ModelState.IsValid)
            {
                return View(log);
            }

            _context.SessionLogs.Update(log);
            _context.SaveChanges();

            return RedirectToAction("Index", new { campaignId = log.CampaignId });
        }

        // POST: /SessionLog/Delete/1
        [HttpPost]
        public IActionResult Delete(int id)
        {
            SessionLog? log = _context.SessionLogs.Find(id);

            if (log != null)
            {
                int campaignId = log.CampaignId;
                _context.SessionLogs.Remove(log);
                _context.SaveChanges();
                return RedirectToAction("Index", new { campaignId = campaignId });
            }

            return RedirectToAction("Index");
        }
    }
}