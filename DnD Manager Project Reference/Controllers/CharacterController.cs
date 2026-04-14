using DnDManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace DnDManager.Controllers
{
    public class CharacterController : Controller
    {
        private readonly AppDbContext _context;

        public CharacterController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Character/Index?campaignId=1
        public IActionResult Index(int campaignId)
        {
            List<Character> characters = _context.Characters
                .Where(c => c.CampaignId == campaignId)
                .ToList();

            ViewBag.CampaignId = campaignId;
            return View(characters);
        }

        // GET: /Character/Details/1
        public IActionResult Details(int id)
        {
            Character? character = _context.Characters.Find(id);

            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // GET: /Character/Create?campaignId=1
        public IActionResult Create(int campaignId)
        {
            ViewBag.CampaignId = campaignId;
            return View();
        }

        // POST: /Character/Create
        [HttpPost]
        public IActionResult Create([Bind("Name,Class,Race,Level,MaxHP,CurrentHP,AC,CampaignId")] Character character)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CampaignId = character.CampaignId;
                return View(character);
            }

            _context.Characters.Add(character);
            _context.SaveChanges();

            return RedirectToAction("Index", new { campaignId = character.CampaignId });
        }

        // GET: /Character/Edit/1
        public IActionResult Edit(int id)
        {
            Character? character = _context.Characters.Find(id);

            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // POST: /Character/Edit/1
        [HttpPost]
        public IActionResult Edit([Bind("CharacterId,Name,Class,Race,Level,MaxHP,CurrentHP,AC,CampaignId")] Character character)
        {
            if (!ModelState.IsValid)
            {
                return View(character);
            }

            _context.Characters.Update(character);
            _context.SaveChanges();

            return RedirectToAction("Index", new { campaignId = character.CampaignId });
        }

        // POST: /Character/Delete/1
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Character? character = _context.Characters.Find(id);

            if (character != null)
            {
                int campaignId = character.CampaignId;
                _context.Characters.Remove(character);
                _context.SaveChanges();
                return RedirectToAction("Index", new { campaignId = campaignId });
            }

            return RedirectToAction("Index");
        }
    }
}