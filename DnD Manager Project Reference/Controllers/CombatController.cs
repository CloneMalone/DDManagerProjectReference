using DnDManager.Data;
using DnDManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace DnDManager.Controllers
{
    public class CombatController : Controller
    {
        private readonly AppDbContext _context;

        public CombatController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Combat/Index?campaignId=1
        // Shows all combatants sorted by initiative (highest goes first)
        public IActionResult Index(int campaignId)
        {
            List<Combatant> combatants = _context.Combatants
                .Where(c => c.CampaignId == campaignId)
                .OrderByDescending(c => c.Initiative)
                .ToList();

            ViewBag.CampaignId = campaignId;
            return View(combatants);
        }

        // GET: /Combat/Create?campaignId=1
        public IActionResult Create(int campaignId)
        {
            ViewBag.CampaignId = campaignId;
            return View();
        }

        // POST: /Combat/Create
        [HttpPost]
        public IActionResult Create([Bind("Name,MaxHP,CurrentHP,Initiative,CampaignId")] Combatant combatant)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CampaignId = combatant.CampaignId;
                return View(combatant);
            }

            _context.Combatants.Add(combatant);
            _context.SaveChanges();

            return RedirectToAction("Index", new { campaignId = combatant.CampaignId });
        }

        // POST: /Combat/UpdateHP
        // Called from the combat tracker to adjust a combatant's current HP
        [HttpPost]
        public IActionResult UpdateHP(int combatantId, int newHP)
        {
            Combatant? combatant = _context.Combatants.Find(combatantId);

            if (combatant == null)
            {
                return NotFound();
            }

            combatant.CurrentHP = newHP;

            if (combatant.CurrentHP <= 0)
            {
                combatant.CurrentHP = 0;
                combatant.IsDefeated = true;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", new { campaignId = combatant.CampaignId });
        }

        // POST: /Combat/ToggleDefeated/1
        [HttpPost]
        public IActionResult ToggleDefeated(int id)
        {
            Combatant? combatant = _context.Combatants.Find(id);

            if (combatant == null)
            {
                return NotFound();
            }

            combatant.IsDefeated = !combatant.IsDefeated;
            _context.SaveChanges();

            return RedirectToAction("Index", new { campaignId = combatant.CampaignId });
        }

        // POST: /Combat/Delete/1
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Combatant? combatant = _context.Combatants.Find(id);

            if (combatant != null)
            {
                int campaignId = combatant.CampaignId;
                _context.Combatants.Remove(combatant);
                _context.SaveChanges();
                return RedirectToAction("Index", new { campaignId = campaignId });
            }

            return RedirectToAction("Index");
        }
    }
}